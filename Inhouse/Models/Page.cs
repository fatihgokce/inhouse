using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public enum PagePosition
    {
        Up, Down
    };
    public class Page
    {
        public long PageId { get; set; }
        public string PageNameTr { get; set; }
        public string PageNameEn { get; set; }
        public int Position { get; set; }
        public string Url { get; set; }
        public string ContextTr { get; set; }
        public string ContextEn { get; set; }
        public bool Active { get; set; }
        public long? ParentId { get; set; }
        public const string RootPageName = "Hepsi";
    }
}