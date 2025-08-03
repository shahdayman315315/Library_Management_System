using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal static class RequiredQueries
    {
        public static List<string> queryList = new List<string>
        {
    "1- List all overdue books and borrowers",
    "2- List top 3 books by average rating",
    "3- List all books by genre and author",
    "4- List borrowers who have never returned a book late",
    "5- List reviews for a book, including reviewer name and rating",
    "6- Books never borrowed",
    "7- Books with no reviews",
    "8- Average rating for each genre",
    "9- Borrowers who reviewed more than 3 books",
    "10- Books borrowed most times in the last year",
    "11- Find author with the most books in the library",
    "12- List authors whose books have never been reviewed"
        };

        public static AppDbContext context;

        // 1- List all overdue books and borrowers.
        public static void OverDueBooks()
        {
           var res= context.BorrowedBooks.Where(bb => bb.ReturnedDate > bb.DueDate).Include(bb=>bb.Book).Include(bb=>bb.Borrower);
            Console.WriteLine("OverDue Books: \n");
            foreach(var borrowedbook in res)
            {
                Console.WriteLine(borrowedbook.Book.Title +" Borrowed by: "+ borrowedbook.Borrower.Name);
            }
        }

        // 2-List top 3 books by average rating.
        public static void Top3AvgRating()
        {
            var res = context.Reviews.GroupBy(r => r.Book.Title);
            var finalres=new Dictionary<string,double>();
            foreach(var group in res)
            {
                 double avg=group.Average(group=>group.Rating);
                finalres.Add(group.Key,avg);
            }
            finalres = finalres.OrderByDescending(b => b.Value).Take(3).ToDictionary();
            Console.WriteLine("Top 3 Books By Average Rating \n");
            foreach(var book in finalres)
            {
                Console.WriteLine(book.Key +"   Average Rating: "+book.Value);
            }
        }

        // 3- List all books by genre and author.
        public static void BooksByGenreAndAuthor()
        {
            //var res = context.Books.Join(context.Authors,
            //    b => b.AuthorId,
            //    a => a.Id,
            //    (b, author) => new
            //    {
            //        Title=b.Title,
            //       genre = b.Genre,
            //        Author = author.Name

            //    });

            var res2 = context.Books.GroupBy(b => new { b.Genre, b.AuthorId });
            Console.WriteLine("Books By Genre And Author \n");
            foreach (var group in res2)
            {
                Console.WriteLine($"Genre: {group.Key.Genre} , Author: {group.Key.AuthorId}");
                foreach(var book in group)
                {
                    Console.WriteLine($" - {book.Title}");
                }
            }
        }

        // 4-List borrowers who have never returned a book late.
        public static void BorrowersNeverLate()
        {
           var res=context.BorrowedBooks.Where(bb=>bb.ReturnedDate<=bb.DueDate).Include(bb=>bb.Borrower);
            Console.WriteLine("Borrowers who have never returned a book late \n");
            foreach(var bb in res)
            {
                Console.WriteLine($"Borrower: {bb.Borrower.Name}");
            }
        }

        // 5-List reviews for a book, including reviewer name and rating.
        public static void ReviewForBooks()
        {
            //can use group join also
            var res1= context.Books.GroupJoin(context.Reviews,
                b => b.Id,
                r => r.BookId,
                (book, reviews) => new
                {
                    Book = book.Title,
                    Reviews = reviews.Join(context.Borrowers,
                        r => r.BorrowerId,
                        borrower => borrower.Id,
                        (r, borrower) => new
                        {
                            Name = borrower.Name,
                            Rating = r.Rating
                        })
                });
            Console.WriteLine("Reviews For Books 1 \n");
            foreach (var book in res1)
            {
                if (!book.Reviews.Any())
                {
                    continue;
                }
                Console.WriteLine($"Book: {book.Book}");
                
                foreach (var review in book.Reviews)
                {
                    Console.WriteLine($"Reviewer: {review.Name}, Rating: {review.Rating}");
                }
            }
            var res = context.Reviews.Include(r =>  r.Borrower).Include(r=>r.Book).GroupBy(r => r.Book.Title);
            Console.WriteLine("Reviews For Books 2 \n");
            foreach(var group in res)
            {
                Console.WriteLine($"Book: {group.Key}");
                foreach (var review in group)
                {
                    Console.WriteLine($"Reviewer: {review.Borrower.Name}, Rating: {review.Rating}");
                }
            }
        }

        // 6- Books never borrowed
        public static void BooksNeverBorrowed()
        {
            var res = from b in context.Books
                      join bb in context.BorrowedBooks on b.Id equals bb.BookId into borrowedBooks
                      from Bb in borrowedBooks.DefaultIfEmpty()
                      select new
                      {

                          Book = b.Title,
                          Borrower = Bb != null ? Bb.Borrower.Name : "Not Borrowed"
                      };
            var finalres = res.Where(b => b.Borrower == "Not Borrowed").Select(b => b.Book);
            if(!finalres.Any())
            {
                Console.WriteLine("No Books Never Borrowed");
                return;
            }
            Console.WriteLine("Books Never Borrowed \n");
            foreach (var book in finalres)
            {
                Console.WriteLine(book);
            }
        }

        // 7- Books with no reviews
        public static void BooksWithNoReviews()
        {
            var res = from b in context.Books
                      join r in context.Reviews on b.Id equals r.BookId into reviews
                      from r in reviews.DefaultIfEmpty()
                      select new
                      {
                          Book = b.Title,
                          Review = r != null ? r.Comment : "No Reviews"
                      };
            var finalres = res.Where(b => b.Review == "No Reviews").Select(b => b.Book);
            if(!finalres.Any())
            {
                Console.WriteLine("No Books With No Reviews");
                return;
            }
            Console.WriteLine("Books With No Reviews \n");
            foreach (var book in finalres)
            {
                Console.WriteLine(book);
            }
        }

        // 8-Average rating for each genre
        public static void AverageRatingForEachGenre()
        {
            var res = context.Books.GroupJoin(context.Reviews,
                b => b.Id,
                R => R.BookId,
                (book, Reviews) => new
                {
                    Genre = book.Genre,
                    Ratings = Reviews.Select(r => r.Rating)
                }).GroupBy(b => b.Genre).Select(g =>
                new
                {
                    Genre = g.Key,
                    AverageRating = g.Average(r => r.Ratings.Any() ? r.Ratings.Average() : 0)
                }
                );
                
            Console.WriteLine("Average Rating For Each Genre \n");
            foreach(var g in res)
            {
                Console.WriteLine($"Genre: {g.Genre} , Average Rating= {g.AverageRating}");

            }
        }
        // 9-Borrowers who reviewed more than 3 books
        public static void BorrowersWhoReviewedMoreThan3Books()
        {
            var res = context.Borrowers.GroupJoin(context.Reviews,
                b => b.Id,
                R => R.BorrowerId,
                (B, Reviews) => new
                {
                    Borrower = B.Name,
                    ReviewCount = Reviews.Count()
                }).Where(b=>b.ReviewCount>3);
            if(!res.Any())
            {
                Console.WriteLine("No Borrowers Reviewed More Than 3 Books");
                return;
            }
            Console.WriteLine("Borrowers Who Reviewed More Than 3 Books \n");
            foreach(var b in res)
            {
                Console.WriteLine(b);
            }
        }

        // 10-Books borrowed most times in the last year
        public static void BooksBorrowedMostTimesInLastYear()
        {
            var res = context.Books.GroupJoin(context.BorrowedBooks,
                b => b.Id,
                bb => bb.BookId,
                (b, bbs) => new
                {
                    book = b.Title,
                    BorrowingTimes =bbs.Any()? bbs.Where(bb=>bb.BorrowedDate.Year==2024).Count():0
                    
                });
           int  maxtimes = res.Max(b => b.BorrowingTimes);
            if( maxtimes == 0)
            {
                Console.WriteLine("No Books were borrowed last year");
                return;
            }
            var finalres=res.Where(b=>b.BorrowingTimes>=maxtimes);
            Console.WriteLine("Book Borrowed Most times");
            foreach( var b in finalres)
            {
                Console.WriteLine(b.book);
            }
        }

        // 11-Find author with the most books in the library.
        public static void AuthorwithTheMostBooks()
        {
            var res = context.Authors.GroupJoin(context.Books,
                a => a.Id,
                b => b.AuthorId,
                (author, books) => new
                {
                    author = author.Name,
                    books=books.Count()
                });
            int maxbooks = res.Max(r => r.books);
            var finalres=res.FirstOrDefault(r=>r.books==maxbooks);
            Console.WriteLine("Author with the Most Books in the Library \n");
            Console.WriteLine(finalres.author);
        }

        // 12-List authors whose books have never been reviewed.
        //  دا مش مظبوط غالبا
        public static void AuthorswithBooksNeverReviewed()
        {
            var reviewedBooksIds=context.Reviews.Select(r=>r.BookId);
            var authors = context.Authors.GroupJoin(context.Books,
                a => a.Id,
                b => b.AuthorId,
                (author, books) => new
                {
                    author = author,
                    books = books
                }).Where(a=>a.books.Any(b=>reviewedBooksIds.Contains(b.Id))).Select(a=>a.author).ToList();
            var finalres = context.Authors.Where(a=>!authors.Contains(a));
            if(finalres.Count() == 0)

            {
                Console.WriteLine("No Books Never Reviewed for any Author.\n");
                return;
            }
            Console.WriteLine("Authors whose books have never been reviewed.\n");
            foreach(var author in finalres)
            {
                
                    Console.WriteLine(author.Name);
                
            }
        }
    }
}
