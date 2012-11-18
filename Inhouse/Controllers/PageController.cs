using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Repositorys;

namespace Inhouse.Controllers
{
    public class PageController : BaseController
    {
        //
        RepositoryPage _repPage;
        public PageController()
        {
            _repPage = new RepositoryPage();
        }
        public ActionResult Index(string lang,long pageId, string pageName)
        {
            var page = _repPage.GetById(pageId);
            AssignLang(lang);
            ViewBag.ChildPages = _repPage.GetChildPages(page.ParentId.Value);
            return View(page);
        }
        

    }
}
