using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;
using Inhouse.Extensions;
using Microsoft.Web.Mvc;
namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class PagePanelController : ManagementBaseController
    {
        //
        // GET: /ManagementPanel/PagePanel/
        public ActionResult ListPage()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ListPage([Bind(Exclude = "PageId")]Page page, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                RepositoryPage rep = new RepositoryPage();
                if (string.IsNullOrEmpty(Request["PageId"]) || Request["PageId"] == "0")
                {
                    Page pg = new Page();
                    pg.Active = page.Active;
                    pg.ContextTr = page.ContextTr.ReplaceIfNotNull("'", "''");
                    pg.ContextEn = page.ContextEn.ReplaceIfNotNull("'", "''"); 
                    pg.PageNameTr = page.PageNameTr;
                    pg.Position = page.Position;
                    pg.PageNameEn = page.PageNameEn;
                    //_mngOyuncu.BeginTransaction();
                    TempData["Success"] = "Kaydedildi";
                    rep.Insert(pg);
                    //_mngOyuncu.CommitTransaction();
                }
                else
                {
                    page.PageId = Request["PageId"].ParseStruct<long>(x => long.Parse(x));
                    page.ContextTr = page.ContextTr.ReplaceIfNotNull("'", "''");
                    page.ContextEn = page.ContextEn.ReplaceIfNotNull("'", "''");
                    TempData["Success"] = "Kaydedildi";
                    rep.Update(page);

                }
            }
            return View();
        }
        public ActionResult NewPage()
        {
            ViewBag.Title = "Yeni Sayfa";

            ViewBag.Pages = new SelectList(new RepositoryPage().GetAll(), "PageId", "PageNameTr");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NewPage([Bind(Exclude = "PageId")]Page page, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                RepositoryPage rep = new RepositoryPage();
                page.ContextTr = page.ContextTr.ReplaceIfNotNull("'", "''");
                page.ContextEn = page.ContextEn.ReplaceIfNotNull("'", "''");
                string spl = frm["PageId"].Split(',')[1];

                page.ParentId = long.Parse(spl);

                page.Position = rep.GetChildMaxPosition(page.ParentId.GetValueOrDefault()) + 1;
                //_mngOyuncu.BeginTransaction();
                TempData["Success"] = "Kaydedildi";
                rep.Insert(page);
            }
            return RedirectToAction("ListPage");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdatePage(Page page, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                RepositoryPage rep = new RepositoryPage();
                string spl = frm["PageId"].Split(',')[1];
                page.ParentId = long.Parse(spl);
                page.ContextTr = page.ContextTr.ReplaceIfNotNull("'", "''");
                page.ContextEn = page.ContextEn.ReplaceIfNotNull("'", "''");
                TempData["Success"] = "Kaydedildi";
                rep.Update(page);
            }
            return RedirectToAction("ListPage");
        }
        public ActionResult UpdatePage(long id)
        {
            RepositoryPage rep = new RepositoryPage();

            Page page = rep.GetById(id);
            ViewBag.parentId = page.ParentId;
            ViewBag.Title = "Güncelleme";
            var selectList = new SelectList(new RepositoryPage().GetAll(), "PageId", "PageNameTr", 2);
            ViewData["pages"] = selectList;
            ViewBag.Pages = new SelectList(new RepositoryPage().GetAll(), "PageId", "PageNameTr", page.ParentId.GetValueOrDefault());
            return View("NewPage", page);
        }
        public ActionResult Delete(long id)
        {
            RepositoryPage rep = new RepositoryPage();
            rep.Delete(id);
            return RedirectToAction("ListPage");
        }
        public virtual ActionResult MoveUp(long id)
        {
            //_catService.SwapPosition(id, CategoryPosition.Up);
            RepositoryPage rep = new RepositoryPage();
            rep.SwapPosition(id, PagePosition.Up);
            return this.RedirectToAction(c => c.ListPage());
        }

        public virtual ActionResult MoveDown(long id)
        {
            RepositoryPage rep = new RepositoryPage();
            rep.SwapPosition(id, PagePosition.Down);
            return this.RedirectToAction(c => c.ListPage());
        }

    }
}
