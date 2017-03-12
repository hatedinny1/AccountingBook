namespace AccountingBook.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AccountingBookModel : DbContext
    {
        public AccountingBookModel()
            : base("name=AccountingBookModel")
        {
        }

        public virtual DbSet<AccountBook> AccountBook { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
