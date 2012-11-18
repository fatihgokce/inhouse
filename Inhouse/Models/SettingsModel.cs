using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class SettingsModel
    {
        public string MetaDescription { get; set; }
        public string MetaKeyboard { get; set; }
        public string AltSloganTr { get; set; }
        public string UstSloganTr { get; set; }
        public string AltSloganEn { get; set; }
        public string UstSloganEn { get; set; }
        public string AltSloganDe { get; set; }
        public string UstSloganDe { get; set; }

        public string EmailAddress { get; set; }
        public string SubjectPrefix { get; set; }
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPEncryption { get; set; }
        public bool SMTPAuthentication { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
    }
}