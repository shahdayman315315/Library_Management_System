using Library_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System
{
    internal class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True; TrustServerCertificate=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId)
                .HasConstraintName("Author_FK").OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<BorrowedBook>(br =>
            {
                br.HasOne(br => br.Book).WithMany(b=>b.BorrowedBooks).HasForeignKey(br=>br.BookId).HasConstraintName("Book_FK").HasPrincipalKey(b=>b.Id);
                br.HasOne(br => br.Borrower).WithMany(b=>b.BorrowedBooks).HasForeignKey(br=>br.BorrowerId).HasConstraintName("Borrower_FK").HasPrincipalKey(b=>b.Id);
                
            });

            modelBuilder.Entity<Review>().HasOne(r => r.Book).WithMany().HasForeignKey(r=>r.BookId).HasConstraintName("ReviewdBook_FK").HasPrincipalKey(b=>b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();
            modelBuilder.Entity<Review>().HasOne(r => r.Borrower).WithMany().HasForeignKey(r=>r.BorrowerId).HasConstraintName("BorrowerReview_FK").HasPrincipalKey(b=>b.Id).OnDelete(DeleteBehavior.Cascade).IsRequired();

        }   
        public DbSet<Book>Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BorrowedBook> BorrowedBooks { get; set; }
        public DbSet<Borrower> Borrowers { get; set; }
        public DbSet<Review> Reviews { get; set; }

       
    }
}
