using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Message
    {
        public long MessageId { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Mesaj{get;set;}
        public DateTime AddedDate { get; set; }
    }
}
