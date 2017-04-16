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
        [Route("skilltreeV2/{year:int?}/{month:int?}")]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountingDetail(int page = 1, int pageSize = 10, int? year = null, int? month = null)
        {
            var objectResult = GetPageOfAccountingBook(page, pageSize, year, month);
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

        private IPagedList<AccountingBookViewModel> GetPageOfAccountingBook(int pageNumber = 1, int pageSize = 10, int? year = null, int? month = null)
        {
            var accountingBookData = GetAccountBookViewModel();

            if (year.HasValue && month.HasValue)
            {
                var startDate = new DateTime(year.Value, month.Value, 1);
                var endDate = startDate.AddMonths(1);
                accountingBookData =
                    GetAccountBookViewModel()
                    .Where(x => x.Date >= startDate.Date && x.Date < endDate.Date);
            }

            return accountingBookData
                   .OrderByDescending(x => x.Date)
                   .ToPagedList(pageNumber, pageSize);
        }

        private IQueryable<AccountingBookViewModel> GetAccountBookViewModel()
        {
            return _accountBookSvc
                .LookupAll()
                .Select(x => new AccountingBookViewModel
                {
                    Category = x.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
                    Date = x.Dateee,
                    Money = x.Amounttt,
                    Remark = x.Remarkkk
                });
        }
    }
}