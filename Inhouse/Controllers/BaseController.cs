using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inhouse.Controllers
{
    public class BaseController : Controller
    {
        public BaseController() {
            ViewBag.Title = "Inhouse";
        }
        public string Lang {
            get { return ViewBag.Lang as string; }
            set { ViewBag.Lang = value; }
        }
        public void AssignLang(string lang)
        {
            if (string.IsNullOrEmpty(lang))
                Lang = "tr";
            else
                Lang = lang;
        }
        public string IsVisit {
            get { return ViewBag.IsVisit as string; }
            set { ViewBag.IsVisit = value; }
        }

    }
}
