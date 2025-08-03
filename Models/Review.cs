﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }
        public  Book Book { get; set; }
        public Borrower Borrower { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; }



    }
}
