using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Book
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Genre { get; set; }
        public int PublishedYear { get; set; }
        public  Author Author { get; set; }
        public  List<BorrowedBook> BorrowedBooks{ get; set; } = new List<BorrowedBook>();


    }
}
