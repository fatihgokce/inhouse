using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Contact
    {
        public long ContactId { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public DateTime AddedDate { get; set; }
    }
}