using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;

namespace Inhouse.Areas.ManagementPanel.Controllers
{
    public class PanelSettingController : Controller
    {
        //
        // GET: /ManagementPanel/PanelSetting/
        RepositorySetting _repSet;
        public PanelSettingController()
        {
            _repSet = new RepositorySetting();
        }
        public ActionResult Setting()
        {
            SettingsModel sm = new SettingsModel();
            sm.MetaDescription = _repSet["MetaDescription"];
            sm.MetaKeyboard = _repSet["MetaKeyboard"];
            //sm.UstSloganTr = _repSet["UstSloganTr"];
            //sm.UstSloganEn = _repSet["UstSloganEn"];
            //sm.UstSloganDe = _repSet["UstSloganDe"];
            //sm.AltSloganTr = _repSet["AltSloganTr"];
            //sm.AltSloganEn = _repSet["AltSloganEn"];
            //sm.AltSloganDe = _repSet["AltSloganDe"];

            sm.EmailAddress = _repSet["EmailAddress"];
            sm.SMTPAuthentication = _repSet["SMTPAuthentication"].Contains("true");
            sm.SMTPEncryption = _repSet["SMTPEncryption"];
            sm.SMTPHost = _repSet["SMTPHost"];
            sm.SMTPPassword = _repSet["SMTPPassword"];
            sm.SMTPPort = _repSet["SMTPPort"];
            sm.SMTPUsername = _repSet["SMTPUsername"];
            sm.SubjectPrefix = _repSet["SubjectPrefix"];
            
            return View(sm);
        }
        [HttpPost]
        public ActionResult Setting(SettingsModel sm, FormCollection frm)
        {
            if (ModelState.IsValid)
            {
                _repSet["MetaDescription"] = sm.MetaDescription;
                _repSet["MetaKeyboard"] = sm.MetaKeyboard;
                //_repSet["UstSloganTr"] = sm.UstSloganTr;
                //_repSet["UstSloganEn"] = sm.UstSloganEn;
                //_repSet["UstSloganDe"] = sm.UstSloganDe;
                //_repSet["AltSloganTr"] = sm.AltSloganTr;
                //_repSet["AltSloganEn"] = sm.AltSloganEn;
                //_repSet["AltSloganDe"] = sm.AltSloganDe;

                _repSet["EmailAddress"] = sm.EmailAddress;
                _repSet["SMTPAuthentication"] = sm.SMTPAuthentication ? "true" : "false";
                _repSet["SMTPEncryption"] = sm.SMTPEncryption;
                _repSet["SMTPHost"] = sm.SMTPHost;
                _repSet["SMTPPassword"] = sm.SMTPPassword;
                _repSet["SMTPPort"] = sm.SMTPPort;
                _repSet["SMTPUsername"] = sm.SMTPUsername;
                _repSet["SubjectPrefix"] = sm.SubjectPrefix;
                TempData["Success"] = "Kaydedildi";
            }

            return RedirectToAction("Setting");
        }

    }
}
