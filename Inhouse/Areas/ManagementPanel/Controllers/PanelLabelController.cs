using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;
using Inhouse.Extensions;
namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class PanelLabelController : Controller
    {
        //
        // GET: /ManagementPanel/PanelLabel/
        RepositoryLabel repLab;
        public PanelLabelController() {
            repLab = new RepositoryLabel();
        }
        public ActionResult ListLabel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListLabel(Label label, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                Label label2 = repLab.GetByKey(label.Key);
                label.ValueEn = label.ValueEn.ReplaceIfNotNull("'","''").ReplaceIfNotNull("@","@@");
                label.ValueTr = label.ValueTr.ReplaceIfNotNull("'", "''").ReplaceIfNotNull("@","@@") ;
                if (!string.IsNullOrEmpty(label2.Key))
                {                    
                    repLab.Update(label);
                }
                else {
                    repLab.Insert(label);
                }
                TempData["Success"] = "Kaydedildi";
            }
            return RedirectToAction("ListLabel");
        }
        public ActionResult Delete(string key)
        {
            repLab.Delete(key);
            return this.RedirectToAction("ListLabel");
        }

    }
}
