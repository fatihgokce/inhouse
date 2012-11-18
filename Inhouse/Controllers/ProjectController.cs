using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Microsoft.Web.Mvc;
using Inhouse.Repositorys;
namespace Inhouse.Controllers
{
    public class ProjectController : BaseController
    {
        //
        // GET: /Project/

        public ActionResult ListProject(string lang)
        {
            //CanUserVisit();
            AssignLang(lang);

            return View();
        }
        public ActionResult Detail(string lang) {
            AssignLang(lang);
            return View();
        }
        public JsonResult InsertContact(string name_surname, string email, string phone)
        {
            if (Request.IsAjaxRequest())
            {
                try
                {
                    if (!CanUserVisit())
                    {
                        RepositoryContact repCon = new RepositoryContact();
                        Contact contact = new Contact
                        {
                            NameSurname = name_surname,
                            Mail = email,
                            Phone = phone,
                            AddedDate = DateTime.Now
                        };
                        repCon.Insert(contact);
                        HttpCookie voteCookie = new HttpCookie("VisitInhouse", "visited");
                        voteCookie.Expires = DateTime.Today.AddMonths(12);
                        Response.Cookies.Add(voteCookie);
                        IsVisit = "true";
                    }
                    return Json(new JsonResponse { Success = true });
                }
                catch (Exception exc)
                {
                    return Json(new JsonResponse
                    {
                        Success = false,
                        Message = exc.Message
                    });

                }
            }
            else
                return null;
        }
        private bool CanUserVisit()
        {
            HttpCookie voteCookie = Request.Cookies["VisitInhouse"];

            if (voteCookie == null)
            {
               
                    IsVisit = "false";
                    return false;
                
            }

            IsVisit = "true";
            return true;
        }

      
    }
}
