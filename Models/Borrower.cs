using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public  List<BorrowedBook> BorrowedBooks { get; set; }= new List<BorrowedBook>();
    }
}
