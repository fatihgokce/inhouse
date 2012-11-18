using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Label
    {
        public virtual string Key { get; set; }
        public virtual string ValueTr { get; set; }
        public virtual string ValueEn { get; set; }
    }
}