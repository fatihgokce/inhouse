using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inhouse.Models;
using Inhouse.Repositorys;
using System.Text;
using Inhouse.Helpers;
using Microsoft.Web.Mvc;
using Inhouse.Controllers;
using Inhouse.Extensions;
namespace Inhouse.Writers
{
    public class NavWriter
    {
        readonly HtmlHelper _htmlHelper;

        StringBuilder _html;
        RepositoryPage rep;
        string lang;
        Dictionary<string,Dictionary<string,string>> dictList;
        public NavWriter(HtmlHelper helper, string lang)
        {
            _htmlHelper = helper;
            _html = new StringBuilder();
            rep = new RepositoryPage();
            this.lang = lang;
            dictList=new Dictionary<string,Dictionary<string,string>>()
            {
                {
                    "tr",
                    new Dictionary<string,string>()
                    {
                    {"MainPage","Ana Sayfa"},
                    {"Projects","Projeler"},
                    {"Career","Kariyer"},
                    {"Contact","İletişim"}
                    }
                },
                {
                    "en",
                    new Dictionary<string,string>()
                    {
                        {"MainPage","Main Page"},
                        {"Projects","Projects"},
                        {"Career","Career"},
                        {"Contact","Contact"}
                    }
                }
            };
            
            
        }
        public IHtmlString Write()
        {
            string current_controller = _htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            var list = rep.GetParentSubCategory(rep.GetRootPage());

            _html.Append(" <div id=\"menu\" style=\"float: right; margin-top: 50px\" class=\"clear-fix\">");
            _html.Append("<ul>");
            _html.Append("  <li class=\"main_li\">{0}".With(
            _htmlHelper.ActionLink<HomeController>(c=>c.Index(lang),dictList[lang]["MainPage"])));
            _html.Append("<ul>");
            _html.Append(" <li class=\"point_li\">");
            _html.Append(" <img src=\"/Content/images/home_06.png\" alt=\"\" /></li>");
            _html.Append(" </ul>");

          

            foreach (var item in list)
            {
                var childs = rep.GetChildPages(item.PageId);
                if (childs.Count == 0)
                {

                    _html.AppendFormat("<li class='main_li'>{0}", 
                        _htmlHelper.ActionLink<PageController>
                        (c => c.Index(lang, item.PageId, GetPageName(lang,item)), GetPageName(lang,item)));
                    _html.Append(@"<ul>
                            <li class='point_li'>
                                <img src='/Content/images/home_06.png' alt='' /></li>
                        </ul>");
                    _html.Append("</li>");
                }
                else
                {
                    _html.AppendFormat(@"<li class='main_li'><a href='javascript:void()'> {0}
                    </a>", GetPageName(lang,item));
                    WriteChild(item);
                  
                    _html.Append("</li>");
                }
            }

            _html.Append("  <li class=\"main_li\">{0}".With(
             _htmlHelper.ActionLink<ProjectController>(c => c.ListProject(lang), dictList[lang]["Projects"])));
            _html.Append("<ul>");
            _html.Append(" <li class=\"point_li\">");
            _html.Append(" <img src=\"/Content/images/home_06.png\" alt=\"\" /></li>");
            _html.Append(" </ul>");
            _html.Append("</li>");

            _html.Append("  <li class=\"main_li\">{0}".With(
           _htmlHelper.ActionLink<CareerController>(c => c.Index(lang), dictList[lang]["Career"])));
            _html.Append("<ul>");
            _html.Append(" <li class=\"point_li\">");
            _html.Append(" <img src=\"/Content/images/home_06.png\" alt=\"\" /></li>");
            _html.Append(" </ul>");
            _html.Append("</li>");

            _html.Append("  <li class=\"main_li\">{0}".With(
         _htmlHelper.ActionLink<ContactController>(c => c.Index(lang), dictList[lang]["Contact"])));
            _html.Append("<ul>");
            _html.Append(" <li class=\"point_li\">");
            _html.Append(" <img src=\"/Content/images/home_06.png\" alt=\"\" /></li>");
            _html.Append(" </ul>");
            _html.Append("</li>");

            _html.Append("</ul></div>");
            return _htmlHelper.Raw(_html.ToString());
        }
       
        void WriteChild(Page page)
        {
            var list = rep.GetChildPages(page.PageId);
            bool first = true;
            foreach (var item in list)
            {
                if (first)
                {
                    first = false;
                    _html.Append("<ul>");
                }
                var childs = rep.GetChildPages(item.PageId);
                if (childs.Count == 0)
                {
                    _html.AppendFormat("<li>{0}</li>", _htmlHelper.ActionLink<PageController>
                        (c => c.Index(lang,item.PageId,GetPageName(lang,item).ConvertWebUrl()),GetPageName(lang,item)));
                }
                else
                {
                    _html.AppendFormat(@"<li class='main_li'><a href='javascript:void()'> {0}
                    </a>", GetPageName(lang,item));
                    WriteChild(item);
                    _html.Append("</li>");
                }
            }
            if (!first)
            {
                _html.Append(@" <li class='point_li'>
                                <img src='/Content/images/home_06.png' alt='' /></li>");
                _html.Append("</ul>");
            }
        }
        private  string GetPageName(string lang, Page page)
        {
            if (lang == "tr")
            {
                return page.PageNameTr;
            }
            else
                return page.PageNameEn;
        }

    }
 
    //private static string MenuName(string lang, Menu menu)
    //    {
    //        if (lang == "tr")
    //        {
    //            return menu.MenuNameTr;
    //        }
    //        else if (lang == "en")
    //            return menu.MenuNameEn;
    //        else
    //            return menu.MenuNameDe;
    //    }
    //    private static string SelectedNav(string source_controller, string dest_conroller)
    //    {
    //        if (source_controller == dest_conroller)
    //            return "class=\"selected-nav\"";
    //        else
    //            return "";
    //    }
}