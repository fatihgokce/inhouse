using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Career
    {
        public long CareerId { get; set; }
        public string NameSurname { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string CvPath { get; set; }
        public DateTime AddedDate { get; set; }
        public string Message { get; set; }
    }
}