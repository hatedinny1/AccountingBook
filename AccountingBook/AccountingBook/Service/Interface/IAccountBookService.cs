using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AccountingBook.Models;

namespace AccountingBook.Service.Interface
{
    public interface IAccountBookService
    {
        IQueryable<AccountBook> LookupAll();
        IQueryable<AccountBook> Query(Expression<Func<AccountBook, bool>> filter);
        AccountBook GetSingle(Expression<Func<AccountBook, bool>> filter);
        void Create(AccountBook entity);
        void Remove(AccountBook entity);
        void Commit();
    }
}
