using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Inhouse.Writers
{
    public class LangNavWriter
    {
        public static string Write(HtmlHelper helper, string lang)
        {
            string current_controller = helper.ViewContext.RouteData.Values["controller"].ToString();
            StringBuilder sb = new StringBuilder("<ul class=\"clear-fix\">");
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            if (lang == "tr")
            {
                sb.AppendFormat("<li><a href=\"{0}\" id=\"lang_bar\">En</a></li>", ConvertNav("en", url));
            }
            else {
                sb.AppendFormat("<li><a href=\"{0}\" id=\"lang_bar\">Tr</a></li>", ConvertNav("tr", url));
            }
            sb.Append(" <li><img src=\"/Content/images/ok.png\" id=\"lang_ok\" /></li>");
            sb.Append("</ul>");
            return sb.ToString();
        }
        private static string ConvertNav(string source, string link)
        {
            if (source == "tr")
            {
                link = link.Replace("/en", "/tr").Replace("/de", "/tr");
                if (!link.Contains("/tr"))
                {
                    link = link + "tr";
                }
            }
            if (source == "en")
            {
                link = link.Replace("/tr", "/en").Replace("/de", "/en");
                if (!link.Contains("/en"))
                {
                    //link = link.Replace("/en", "").Replace("/de", "");
                    link = link + "en";
                }
            }
           
            return link;
        }
        private static string SelectedNav(string lang, string source)
        {
            if (lang == source)
                return "class=\"selected-li\"";
            else
                return "";
        }
    }
}