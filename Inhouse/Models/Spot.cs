using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Spot
    {
        public long SpotId { get; set; }
        public string Path { get; set; }
        public bool Active { get; set; }
    }
}