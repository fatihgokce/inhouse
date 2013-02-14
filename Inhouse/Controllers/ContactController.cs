using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Helpers;
using Inhouse.Models;
using Inhouse.Repositorys;
namespace Inhouse.Controllers
{
    public class ContactController : BaseController
    {
        //
        // GET: /Contact/

        public ActionResult Index(string lang)
        {
            AssignLang(lang);

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(FormCollection frm, string lang)
        {
            AssignLang(lang);
          
            Message message = new Message
            {
                AddedDate = DateTime.Now,
                Mail = frm["email"],
                NameSurname = frm["name_surname"],
                Phone = frm["phone"],
                Mesaj = frm["message"]
                
            };

            RepositoryMessage repContact = new RepositoryMessage();
            repContact.Insert(message);
            RepositoryLabel repLabel = new RepositoryLabel();
            Label label = repLabel.GetByKey("ContactMessage");
            string messageContact = "";
            if (lang == "tr")
                messageContact = label.ValueTr;
            else
                messageContact = label.ValueEn;
            TempData["succedd"] = messageContact;

            Global.SendMail(new Contact 
            { 
                NameSurname=message.NameSurname,
                Mail=message.Mail
            },message.Mesaj);
            return RedirectToAction("Succeed", "Home", new { lang = lang });
        }
       

    }
}
