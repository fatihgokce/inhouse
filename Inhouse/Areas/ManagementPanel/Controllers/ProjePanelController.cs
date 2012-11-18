using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Repositorys;
using Inhouse.Models;
using Inhouse.Extensions;
using Inhouse.Services;
namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class ProjePanelController : ManagementBaseController
    {
        //
        // GET: /ManagementPanel/ProjePanel/
        RepositoryProje repProje;
        public ProjePanelController()
        {
            repProje = new RepositoryProje();
        }
        public ActionResult ListProje(int? page)
        {
            int totalCount;           
            RepositorySetting settin = new RepositorySetting();
            int pageCount =int.Parse(settin["PageCount"].ToString());
            List<Proje> lists = repProje.GetListByDate(pageCount,page ?? 1,out totalCount);
            PaginatedList pager = new PaginatedList((page ?? 1), pageCount, totalCount);
            ViewBag.Pager = pager;
            ViewBag.Title = "Yeni Proje";
            return View(lists);
        }
        public ActionResult NewProje() {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NewProje([Bind(Exclude = "ProjeId")]Proje proje, FormCollection frm)
        {
            if (ModelState.IsValid)
            {

                proje.AciklamaTr.ReplaceIfNotNull("'","''");
                proje.AciklamaEn.ReplaceIfNotNull("'", "''");
                proje.LokasyonEn.ReplaceIfNotNull("'","''");
                proje.LokasyonTr.ReplaceIfNotNull("'", "''");
                var _httpFileServ = new HttpFileService("", new ImageService());
                var images = _httpFileServ.GetUploadedImages(Request, true, true, false);
               
                int index = 0;
                foreach (var item in images)
                {

                    if (index == 0)
                        proje.PicturePath1 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 1)
                        proje.PicturePath2 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 2)
                        proje.PicturePath3 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 3)
                        proje.PicturePath4 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 4)
                        proje.PicturePath5 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 5)
                        proje.PicturePath6 = string.Format("/upload/{0} ", item.MainFileName);
                    index++;
                }
                proje.Tarih = DateTime.Now;
                repProje.Insert(proje);
                
                TempData["Success"] = "Kaydedildi";
                return RedirectToAction("ListProje");

            }
            ViewBag.Title = "Yeni Proje";
            
            return View();
        }

        public ActionResult UpdateProje(long id) {
            //Request["ProjeId"].ParseStruct<long>(x => long.Parse(x));
            Proje prj = repProje.GetById(id);
            return View("NewProje",prj);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult UpdateProje(Proje proje, FormCollection frm)
        {
           
            if (ModelState.IsValid)
            {

                proje.AciklamaTr= proje.AciklamaTr.ReplaceIfNotNull("'", "''");
                proje.AciklamaEn= proje.AciklamaEn.ReplaceIfNotNull("'", "''");
                proje.LokasyonEn= proje.LokasyonEn.ReplaceIfNotNull("'", "''");
                proje.LokasyonTr= proje.LokasyonTr.ReplaceIfNotNull("'", "''");
                var _httpFileServ = new HttpFileService("", new ImageService());
                var images = _httpFileServ.GetUploadedImages(Request, true, true, false);
                Proje oldProje = repProje.GetById(proje.ProjeId);
                proje.PicturePath1 = oldProje.PicturePath1;
                proje.PicturePath2 = oldProje.PicturePath2;
                proje.PicturePath3 = oldProje.PicturePath3;
                proje.PicturePath4 = oldProje.PicturePath4;
                proje.PicturePath5 = oldProje.PicturePath5;
                proje.PicturePath6 = oldProje.PicturePath6;
                int index = 0;
                foreach (var item in images)
                {

                    if (index == 0)
                        proje.PicturePath1 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 1)
                        proje.PicturePath2 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 2)
                        proje.PicturePath3 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 3)
                        proje.PicturePath4 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 4)
                        proje.PicturePath5 = string.Format("/upload/{0} ", item.MainFileName);
                    else if (index == 5)
                        proje.PicturePath6 = string.Format("/upload/{0} ", item.MainFileName);
                    index++;
                }
               
                repProje.Update(proje);

                TempData["Success"] = "Kaydedildi";
                return RedirectToAction("ListProje");

            }
            ViewBag.Title = "Güncelleme";

            return View("NewProje",proje);
           
        }
        public ActionResult Delete(long id) {
            return RedirectToAction("ListProje");
        }

    }
}
