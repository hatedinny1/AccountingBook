using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingBook.Controllers
{
    public class ValidateController : Controller
    {
        // GET: Validate
        public ActionResult NotAfterToday(DateTime date, object value = null)
        {
            var isValidate = date.Date.CompareTo(DateTime.Now.Date) <= 0;
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}