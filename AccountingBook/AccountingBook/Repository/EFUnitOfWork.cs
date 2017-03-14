using System.Data.Entity;
using AccountingBook.Models;
using AccountingBook.Repository.Interface;

namespace AccountingBook.Repository
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public EFUnitOfWork(DbContext dbContext)
        {
            Context = dbContext;
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}