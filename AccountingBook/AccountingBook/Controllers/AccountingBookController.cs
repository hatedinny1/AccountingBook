using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Models.Enum;
using AccountingBook.Models.ViewModel;
using AccountingBook.Repository;
using AccountingBook.Repository.Interface;
using AccountingBook.Service;
using AccountingBook.Service.Interface;
using PagedList;

namespace AccountingBook.Controllers
{
    public class AccountingBookController : Controller
    {
        private readonly IAccountBookService _accountBookSvc;

        public AccountingBookController(IAccountBookService accountBookSvc)
        {
            _accountBookSvc = accountBookSvc;
        }

        // GET: AccountingBook
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountingDetail(int page = 1, int pageSize = 10)
        {
            var objectResult = _accountBookSvc
                               .LookupAll()
                               .Select(x => new AccountingBookViewModel
                               {
                                   Category = x.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
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