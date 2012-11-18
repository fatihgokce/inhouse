using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;

namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class SpotPanelController : ManagementBaseController
    {
        //
        // GET: /ManagementPanel/SpotPanel/

        public ActionResult ListSpot()
        {
            return View();
        }
        public ActionResult NewSpot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateSpot(Spot spot, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                RepositorySpot rep = new RepositorySpot();
                string pic_path = uploadPicture();
                if (!string.IsNullOrEmpty(pic_path))
                    spot.Path = pic_path;

                rep.Update(spot);
                return RedirectToAction("ListSpot");
            }
            return View();
        }
        [HttpPost]
        public ActionResult NewSpot([Bind(Exclude = "SpotId")]Spot spot, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                spot.Path = uploadPicture();
                RepositorySpot rep = new RepositorySpot();
                rep.Insert(spot);
                return RedirectToAction("ListSpot");
            }
            return View();
        }
        public ActionResult Delete(long id)
        {
            RepositorySpot repSpot = new RepositorySpot();
            repSpot.Delete(id);
            return RedirectToAction("ListSpot");
        }
        public ActionResult UpdateSpot(long id)
        {
            RepositorySpot repSpot = new RepositorySpot();
            var item = repSpot.GetById(id);
            return View("NewSpot", item);
        }
        string uploadPicture()
        {
            string retVal = "";
            if (Request.Files["film_image1"].ContentLength > 0)
            {
                string uploadPath = Server.MapPath("/upload/") + Request.Files["film_image1"].FileName;
                Request.Files["film_image1"].SaveAs(uploadPath);
                retVal = "/upload/" + Request.Files["film_image1"].FileName;
            }
            return retVal;
        }
    }
}
