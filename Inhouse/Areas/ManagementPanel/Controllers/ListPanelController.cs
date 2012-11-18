using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Repositorys;
using Inhouse.Models;
namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class ListPanelController : ManagementBaseController
    {
        //
        // GET: /ManagementPanel/ListPanel/

        public ActionResult ListKariyer()
        {
            return View();
        }
        public ActionResult ListContact() {
            return View();
        } 

    }
}
