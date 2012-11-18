using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using Inhouse.Models;
using Inhouse.Repositorys;
namespace Inhouse.Helpers
{
    public static class Global
    {
        public static void SendMail(Contact contact,string mesaj) {
            try
            {
                RepositorySetting setting = new RepositorySetting();
                
                var mailMessage = new MailMessage(setting["SMTPUsername"], setting["EmailAddress"])
                {
                    Subject = contact.Mail + " den ",
                    Body = contact.NameSurname + " mesaj:" + mesaj
                };

                var smtpClient = new SmtpClient(setting["SMTPHost"], setting["SMTPPort"].ParseStruct<int>(x => int.Parse(x)));
                smtpClient.EnableSsl = setting["SMTPEncryption"] == "SSL" ? true : false;
                if (setting["SMTPAuthentication"].Contains("true"))
                {
                    smtpClient.Credentials = new NetworkCredential(setting["SMTPUsername"], setting["SMTPPassword"]);
                }
                else
                    smtpClient.Credentials = CredentialCache.DefaultNetworkCredentials;
                smtpClient.Send(mailMessage);
            }
            catch
            {
            }
        }
    }
}