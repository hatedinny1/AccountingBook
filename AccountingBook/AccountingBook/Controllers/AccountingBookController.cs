using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Models.ViewModel;
using AccountingBook.Repository;
using AccountingBook.Service;
using PagedList;

namespace AccountingBook.Controllers
{
    public class AccountingBookController : Controller
    {
        private readonly AccountBookService _accountBookSvc;
        public AccountingBookController()
        {
            var unitOfWork = new EFUnitOfWork();
            _accountBookSvc = new AccountBookService(unitOfWork);
        }
        // GET: AccountingBook
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountingDetail(int page = 1)
        {
            var pageSize = 10;
            var objectResult = _accountBookSvc
                               .LookupAll()
                               .Select(x => new AccountingBookViewModel
                {
                                   Category = x.Categoryyy % 2 == 0 ? "收入" : "支出",
                                   Date = x.Dateee,
                                   Money = x.Amounttt,
                                   Remark = x.Remarkkk
                               })
                               .OrderBy(x => x.Date)
                               .ToPagedList(page, pageSize);
            return View(objectResult);
        }        
    }
}