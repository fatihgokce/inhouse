using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;

namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class UploadPanelController : ManagementBaseController
    {
        //
        // GET: /ManagementPanel/Upload/
        RepositoryUpload _rep;
        public UploadPanelController()
        {
            _rep = new RepositoryUpload();
        }
        public ActionResult ListUpload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ListUpload(FormCollection frm)
        {
            Upload up = new Upload();
            up.Path = uploadPicture();
            _rep.Insert(up);
            return RedirectToAction("ListUpload");
        }
        public ActionResult Delete(long id)
        {
            _rep.Delete(id);
            return this.RedirectToAction("ListUpload");
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
