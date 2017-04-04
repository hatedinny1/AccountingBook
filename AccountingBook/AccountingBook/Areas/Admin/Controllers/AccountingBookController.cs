using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AccountingBook.Filters;
using AccountingBook.Models;
using AccountingBook.Models.Enum;
using AccountingBook.Service.Interface;
using AccountingBook.Models.ViewModel;
using PagedList;

namespace AccountingBook.Areas.Admin.Controllers
{
    [AuthorizeAdminOnly(Users = "hatedinny1@gmail.com")]
    public class AccountingBookController : Controller
    {
        private readonly IAccountBookService _accountBookSvc;

        public AccountingBookController(IAccountBookService accountBookSvc)
        {
            _accountBookSvc = accountBookSvc;
        }

        // GET: Admin/AccountingBook
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
                                   Id = x.Id,
                                   Category = x.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
                                   Date = x.Dateee,
                                   Money = x.Amounttt,
                                   Remark = x.Remarkkk
                               })
                               .OrderByDescending(x => x.Date)
                               .ToPagedList(page, pageSize);
            return PartialView(objectResult);
        }

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
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _accountBookSvc.GetSingle(x => x.Id == id);
            if (model == default(AccountBook))
            {
                return HttpNotFound();
            }

            var viewModel = BindAccountingBookViewModelToModel(model);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountingBookViewModel accountingBookViewModel)
        {
            if (ModelState.IsValid)
            {
                var source = _accountBookSvc.GetSingle(x => x.Id == accountingBookViewModel.Id);
                source.Categoryyy = (int)accountingBookViewModel.Category;
                source.Amounttt = accountingBookViewModel.Money;
                source.Dateee = accountingBookViewModel.Date;
                source.Remarkkk = accountingBookViewModel.Remark;
                _accountBookSvc.Commit();
                return RedirectToAction("Index");
            }
            return View(accountingBookViewModel);
        }

        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = _accountBookSvc.GetSingle(x => x.Id == id);
            if (model == default(AccountBook))
            {
                return HttpNotFound();
            }

            var viewModel = BindAccountingBookViewModelToModel(model);
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(Guid? id)
        {
            var model = _accountBookSvc.GetSingle(x => x.Id == id);
            _accountBookSvc.Remove(model);
            _accountBookSvc.Commit();
            return RedirectToAction("Index");
        }

        private static AccountingBookViewModel BindAccountingBookViewModelToModel(AccountBook model)
        {
            return new AccountingBookViewModel
            {
                Category = model.Categoryyy == 0 ? CategoryEnum.Expenditure : CategoryEnum.Income,
                Date = model.Dateee,
                Money = model.Amounttt,
                Remark = model.Remarkkk
            };
        }
    }
}