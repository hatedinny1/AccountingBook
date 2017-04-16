using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Filters;
using AccountingBook.Models;
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
        public ActionResult AccountingDetail(int page = 1, int pageSize = 10, int? year = null, int? month = null)
        {
            var accountingBookData = _accountBookSvc
                .LookupAll()
                .Select(x => new AccountingBookViewModel
                {
                    Category = x.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
                    Date = x.Dateee,
                    Money = x.Amounttt,
                    Remark = x.Remarkkk
                });

            if (year.HasValue && month.HasValue)
            {
                var startDate = new DateTime(year.Value, month.Value, 1);
                var endDate = startDate.AddMonths(1);
                accountingBookData = accountingBookData.Where(x => x.Date >= startDate.Date && x.Date < endDate.Date);
            }

            var objectResult = accountingBookData
                               .OrderByDescending(x => x.Date)
                               .ToPagedList(page, pageSize);
            return View(objectResult);
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
            return RedirectToAction("Index");
        }
    }
}