using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
    }
}