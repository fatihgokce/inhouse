using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;
using Inhouse.Extensions;
namespace Inhouse.Controllers
{
    public class CareerController : BaseController
    {
        //
        // GET: /Career/

        public ActionResult Index(string lang)
        {
            AssignLang(lang);
            ViewBag.Title = "Inhouse";
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(FormCollection frm,string lang)
        {
            AssignLang(lang);
            string cv_path=uploadFile();
            Career career = new Career
            {
                AddedDate=DateTime.Now,
                CvPath=cv_path,
                Mail = frm["email"],
                NameSurname = frm["name_surname"],
                Phone=frm["phone"],
                Position = frm["job_option"],
                Message = frm["message"]
            };
            RepositoryCareer repCareer = new RepositoryCareer();
            repCareer.Insert(career);
            RepositoryLabel repLabel = new RepositoryLabel();
            Label label = repLabel.GetByKey("CvMessage");
            string message = "";
            if (lang == "tr")
                message = label.ValueTr;
            else
                message = label.ValueEn;
            TempData["succedd"] = message ;
            return RedirectToAction("Succeed", "Home", new { lang=lang});
        }
        string uploadFile()
        {
            string retVal = "";
            if (Request.Files["file_cv"].ContentLength > 0)
            {
                if (IsValidFile(Request.Files["file_cv"]))
                {
                    string uploadPath = Server.MapPath("/upload/") + Request.Files["file_cv"].FileName;
                    Request.Files["file_cv"].SaveAs(uploadPath);
                    retVal = "/upload/" + Request.Files["file_cv"].FileName;
                }
            }
            return retVal;
        }
        private bool IsValidFile(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            return (extension == ".pdf" || extension == ".txt"
                || extension == ".doc" || extension == ".docx"
                || extension == ".xls" || extension == ".xlsx");
        }

    }
}
