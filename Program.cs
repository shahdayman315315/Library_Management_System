using Azure.Core;
using Library_Management_System.Models;

namespace Library_Management_System
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            var context = new AppDbContext();
            /*
            var author = new Author
            {
                Name = "John Doe",
                BirthDate = new DateTime(1980, 1, 1),
                Books = new List<Book>
                {
                    new Book
                    {
                        Title = "First Book",
                        Genre = "Fiction",
                        PublishedYear = 2020,
                    },
                    new Book
                    {
                        Title = "Second Book",
                        Genre = "Novel",
                        PublishedYear = 2021,
                    },
                    new Book
                    {
                        Title = "Third Book",
                        Genre = "Horror",
                        PublishedYear = 2021,
                    }

                }
            };
            var author2 = new Author
            {
                Name = "Second Author",
                BirthDate = new DateTime(2000, 2, 2),
                Books = new List<Book>
                {
                    new Book
                    {
                        Title = "Fourth Book",
                        Genre = "Science Fiction",
                        PublishedYear = 2022,
                    },
                    new Book
                    {
                        Title = "Fifth Book",
                        Genre = "Mystery",
                        PublishedYear = 2025,
                    },
                    new Book
                    {
                        Title = "Sixth Book",
                        Genre = "Narrative",
                        PublishedYear = 2025,
                    }

                }
            };

            context.Authors.Add(author);
            context.Authors.Add(author2);
            context.SaveChanges();

            //update Author
            var updatedAuthor = new Author
            {
                Id = 1,
                Name = "First Author",

            };
            context.Update(updatedAuthor);
            context.Entry(updatedAuthor).Property(a => a.BirthDate).IsModified = false; // Only update Name, not BirthDate
            context.SaveChanges();

            //delete Author  كنت رنيت البرنامج تانى ف ضاف نفس الكاتبين والاى دى مختلف بس ف عملت عليهم الديليت بقا
            var deletedauthors = context.Authors.Where(a => a.Id > 2).ToList();
            context.Authors.RemoveRange(deletedauthors);
            context.SaveChanges();
            
            
            var borrowers = new List<Borrower>
            {
               new Borrower
               {
                 Name = "Ahmed",
                Email = "ahmed315@gmail.com"

               },
               new Borrower
               {
                  Name = "shahd",
                Email = "shahd725@gmail.com"

               },
               new Borrower
               {
                   Name="Noor",
                   Email = "noor615@gmail.com"

               }
            };

            context.Borrowers.AddRange(borrowers);
            context.SaveChanges();
            
            var borrowedbooks = new List<BorrowedBook>
            {
             new BorrowedBook
             {

                 Book= context.Books.FirstOrDefault(b => b.Id == 1),
                 Borrower= context.Borrowers.FirstOrDefault(b => b.Id == 1),
                 BorrowedDate = DateOnly.FromDateTime(DateTime.Now),
                 DueDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(14),
                 ReturnedDate = null
             },
             new BorrowedBook
             {
                    Book= context.Books.FirstOrDefault(b => b.Id == 2),
                    Borrower= context.Borrowers.FirstOrDefault(b => b.Id == 2),
                    BorrowedDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(1),
                    DueDate =  DateOnly.FromDateTime(DateTime.Now).AddDays(15),
                    ReturnedDate = null

             },
             new BorrowedBook
             {
                    Book= context.Books.FirstOrDefault(b => b.Id == 3),
                    Borrower= context.Borrowers.FirstOrDefault(b => b.Id == 1),
                    BorrowedDate = new DateOnly(2025,7,10),
                    DueDate = new DateOnly(2025,7,24),
                    ReturnedDate = null
             },
             new BorrowedBook
             {
                    Book= context.Books.FirstOrDefault(b => b.Id == 1),
                    Borrower= context.Borrowers.FirstOrDefault(b => b.Id == 2),
                    BorrowedDate = new DateOnly(2025,5,5),
                    DueDate = new DateOnly(2025,5,20),
                    ReturnedDate = null
             },
             new BorrowedBook
             {
                    Book= context.Books.FirstOrDefault(b => b.Id == 4),
                    Borrower= context.Borrowers.FirstOrDefault(b => b.Id == 3),
                    BorrowedDate = new DateOnly(2025,6,25),
                    DueDate = new DateOnly(2025,7,2),
                    ReturnedDate = null
             }
            };
            context.BorrowedBooks.AddRange(borrowedbooks);
            context.SaveChanges();

            var borrowedBook = context.BorrowedBooks.FirstOrDefault(bb => bb.Book.Id == 1 && bb.Borrower.Id == 2);
            borrowedBook.ReturnedDate = new DateOnly(2025, 5, 15);
            var borrowedBook2 = context.BorrowedBooks.FirstOrDefault(bb => bb.Book.Id == 4 && bb.Borrower.Id == 3);
            borrowedBook2.ReturnedDate = new DateOnly(2025, 7, 1);
            var borrowedBook3 = context.BorrowedBooks.FirstOrDefault(bb => bb.Book.Id == 1 && bb.Borrower.Id == 1);
            borrowedBook3.ReturnedDate = new DateOnly(2025, 8, 20);
            var borrowedBook4 = context.BorrowedBooks.FirstOrDefault(bb => bb.Book.Id == 2 && bb.Borrower.Id == 2);
            borrowedBook4.ReturnedDate = new DateOnly(2025, 9, 20);
             context.SaveChanges();

            
            var Reviews = new List<Review>
            {
                new Review
                {
                    Book = context.Books.FirstOrDefault(b => b.Id == 1),
                    Borrower = context.Borrowers.FirstOrDefault(b => b.Id == 1),
                    Rating = 9,
                    Comment = "Excellent book!",
                    ReviewDate = DateTime.Now
                },
                new Review
                {
                    Book = context.Books.FirstOrDefault(b => b.Id == 2),
                    Borrower = context.Borrowers.FirstOrDefault(b => b.Id == 2),
                    Rating = 7,
                    Comment = "Good Book but didn't make me Enthusiatic.", 
                    ReviewDate = DateTime.Now.AddDays(5)
                },
                new Review
                {
                    Book = context.Books.FirstOrDefault(b => b.Id == 3),
                    Borrower = context.Borrowers.FirstOrDefault(b => b.Id == 1),
                    Rating = 8,
                    Comment = "Interesting read, but could be better.",
                    ReviewDate = new DateTime(2025,7,1)
                },
                new Review
                {
                    Book = context.Books.FirstOrDefault(b => b.Id == 4),
                    Borrower = context.Borrowers.FirstOrDefault(b => b.Id == 3),
                    Rating = 6,
                    Comment = "Loved the plot and characters!",
                    ReviewDate = new DateTime(2025, 7, 2)
                },
               new Review
                {
            
                    Book = context.Books.FirstOrDefault(b => b.Id ==1),
                    Borrower = context.Borrowers.FirstOrDefault(b => b.Id == 2),
                    Rating = 9,
                    Comment = "I like this book.",
                    ReviewDate = new DateTime(2025,5, 15)
                }

            };
            
            context.Reviews.AddRange(Reviews);
            context.SaveChanges();
            */


            RequiredQueries.context = context;
            Console.WriteLine("Choose Query Number: \n");
           foreach(var q in RequiredQueries.queryList)
            {
                Console.WriteLine(q);
            }
            int queryNumber;
            while (!int.TryParse(Console.ReadLine(), out queryNumber) || queryNumber < 1 || queryNumber > RequiredQueries.queryList.Count)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and " + RequiredQueries.queryList.Count);
            }   
            Console.WriteLine();

            switch (queryNumber)
            {
                case 1:
                    RequiredQueries.OverDueBooks();
                    break;
                case 2:
                    RequiredQueries.Top3AvgRating();
                    break;
                case 3:
                    RequiredQueries.BooksByGenreAndAuthor();
                    break;
                case 4:
                    RequiredQueries.BorrowersNeverLate();
                    break;
                case 5:
                    RequiredQueries.ReviewForBooks();
                    break;
                case 6:
                    RequiredQueries.BooksNeverBorrowed();
                    break;
                case 7:
                    RequiredQueries.BooksWithNoReviews();
                    break;
                case 8:
                    RequiredQueries.AverageRatingForEachGenre();
                    break;
                case 9:
                    RequiredQueries.BorrowersWhoReviewedMoreThan3Books();
                    break;
                case 10:
                    RequiredQueries.BooksBorrowedMostTimesInLastYear();
                    break;
                case 11:
                    RequiredQueries.AuthorwithTheMostBooks();
                    break;
                case 12:
                    RequiredQueries.AuthorswithBooksNeverReviewed();
                    break;
                

            }

        }
    }
}
