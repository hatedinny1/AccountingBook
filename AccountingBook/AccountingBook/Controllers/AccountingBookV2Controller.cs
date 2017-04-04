using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Filters;
using AccountingBook.Models;
using AccountingBook.Models.Enum;
using AccountingBook.Models.ViewModel;
using AccountingBook.Service.Interface;
using PagedList;

namespace AccountingBook.Controllers
{
    public class AccountingBookV2Controller : Controller
    {
        private readonly IAccountBookService _accountBookSvc;

        public AccountingBookV2Controller(IAccountBookService accountBookSvc)
        {
            _accountBookSvc = accountBookSvc;
        }

        // GET: AccountingBookV2
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountingDetail(int page = 1, int pageSize = 10)
        {
            var objectResult = GetPageOfAccountingBook(page, pageSize);
            return PartialView(objectResult);
        }

        [AuthorizePlus]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountingBookViewModel accountingBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var model = new AccountBook()
                {
                    Id = Guid.NewGuid(),
                    Categoryyy = (int)accountingBookViewModel.Category,
                    Amounttt = accountingBookViewModel.Money,
                    Dateee = accountingBookViewModel.Date,
                    Remarkkk = accountingBookViewModel.Remark
                };
                this._accountBookSvc.Create(model);
                this._accountBookSvc.Commit();
            }

            var objectResult = GetPageOfAccountingBook();
            return PartialView("AccountingDetail", objectResult);
        }

        private IPagedList<AccountingBookViewModel> GetPageOfAccountingBook(int pageNumber = 1, int pageSize = 10)
        {
            return _accountBookSvc
                   .LookupAll()
                   .Select(x => new AccountingBookViewModel
                   {
                       Category = x.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
                       Date = x.Dateee,
                       Money = x.Amounttt,
                       Remark = x.Remarkkk
                   })
                   .OrderByDescending(x => x.Date)
                   .ToPagedList(pageNumber, pageSize);
        }
    }
}