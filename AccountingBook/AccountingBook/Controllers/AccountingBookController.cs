using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Models.ViewModel;

namespace AccountingBook.Controllers
{
    public class AccountingBookController : Controller
    {
        // GET: AccountingBook
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult AccountingDetail()
        {
            var accountingDetail = GetAccountingBookViewModels();
            return View(accountingDetail);
        }

        #region FakeData

        private static List<AccountingBookViewModel> GetAccountingBookViewModels()
        {
            var accountingDetail = new List<AccountingBookViewModel>()
            {
                new AccountingBookViewModel()
                {
                    Category = "支出",
                    Date = new DateTime(2016, 1, 1),
                    Money = 300,
                    Remark = "信用卡費"
                },
                new AccountingBookViewModel()
                {
                    Category = "支出",
                    Date = new DateTime(2016, 1, 2),
                    Money = 1600
                },
                new AccountingBookViewModel()
                {
                    Category = "支出",
                    Date = new DateTime(2016, 1, 3),
                    Money = 800
                },
            };
            return accountingDetail;
        }

        #endregion
    }
}