using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Inhouse.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(string lang)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            AssignLang(lang);
            ViewBag.Title = "Inhouse";
            //return RedirectToAction("Detail", "Project", new { lang="tr"});
            return View();
        }
       
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
           
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        public ActionResult Kurumsal(string lang) {
            AssignLang(lang);
            return View();
        }
        public ActionResult Succeed(string lang)
        {
            ViewBag.Title = "Inhouse";
            return View();
        }
    }
}
