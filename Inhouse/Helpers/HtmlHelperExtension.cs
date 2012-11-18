using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Inhouse.Writers;
using Microsoft.Web.Mvc;
using System.Web.Routing;
using mwm = Microsoft.Web.Mvc.Internal;
using Inhouse.Models;
using Inhouse.Repositorys;
using System.Text;
namespace Inhouse.Helpers
{
    public static class HtmlHelperExtension
    {
        public static IDisposable MultipartForm(this HtmlHelper helper)
        {
            string action = helper.ViewContext.RouteData.GetRequiredString("action");
            string controller = helper.ViewContext.RouteData.GetRequiredString("controller");
            return helper.BeginForm(action, controller, FormMethod.Post, new { enctype = "multipart/form-data" });
        }
        public static string BuildUrlFromExpression<TController>(RequestContext context, RouteCollection routeCollection, Expression<Action<TController>> action) where TController : Controller
        {
            RouteValueDictionary routeValues = mwm.ExpressionHelper.GetRouteValuesFromExpression(action);
            VirtualPathData vpd = routeCollection.GetVirtualPathForArea(context, routeValues);//.GetVirtualPath(context, routeValues);
            return (vpd == null) ? null : vpd.VirtualPath;
        }
        public static string BuildUrlFromExpressionFG<TController>(this HtmlHelper helper, Expression<Action<TController>> action) where TController : Controller
        {
            return BuildUrlFromExpression<TController>(helper.ViewContext.RequestContext, helper.RouteCollection, action);
        }
        public static IHtmlString WriteLangNav(this HtmlHelper helper, string lang)
        {
            return helper.Raw(LangNavWriter.Write(helper, lang));
        }
        public static string UpArrowLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action) where T : Controller
        {
            return string.Format("<a href=\"{0}\" class=\"arrowlink\">{1}</a>", htmlHelper.BuildUrlFromExpressionFG<T>(action),
                htmlHelper.Image("~/Content/images/Up.png"));
        }

        public static string DownArrowLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action) where T : Controller
        {
            return string.Format("<a href=\"{0}\" class=\"arrowlink\">{1}</a>",
                htmlHelper.BuildUrlFromExpressionFG<T>(action),
                htmlHelper.Image("~/Content/images/Down.png"));
        }
        public static IHtmlString WritePages(this HtmlHelper helper, Page rootPage)
        {
            PageWriter writer = new PageWriter(rootPage, helper);
            return helper.Raw(writer.Write());
        }
        public static string ImageLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action, string imgSrc) where T : Controller
        {
            return string.Format("<a href=\"{0}\" >{1}</a>", BuildUrlFromExpression<T>(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, action),
               htmlHelper.Image(imgSrc));
            //return string.Format("<a href=\"{0}\" width=\"96\" height=\"72\">{1}</a>", htmlHelper.BuildUrlFromExpression<T>(action),
            //  htmlHelper.Image(imgSrc));
        }
        public static string ImageLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action, string imgSrc, string htmlAttributes, object imgAttiribute) where T : Controller
        {
            return string.Format("<a href=\"{0}\" {2} >{1}</a>", BuildUrlFromExpression<T>(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, action),
               htmlHelper.Image(imgSrc, imgAttiribute), htmlAttributes);
        }
        public static string ImageLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action, string imgSrc, string htmlAttributes, object imgAttiribute, string target) where T : Controller
        {
            return string.Format("<a href=\"{0}\" {2} target=\"{3}\" >{1}</a>", BuildUrlFromExpression<T>(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, action),
               htmlHelper.Image(imgSrc, imgAttiribute), htmlAttributes, target);
        }
        public static string ImageLink<T>(this HtmlHelper htmlHelper, Expression<Action<T>> action, string routeValue, string imgSrc, string htmlAttributes) where T : Controller
        {
            string url = BuildUrlFromExpression<T>(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection, action) + routeValue;
            return string.Format("<a href=\"{0}\" {2} >{1}</a>", url,
               htmlHelper.Image(imgSrc), htmlAttributes);
        }
        public static IHtmlString MetaDescription(this HtmlHelper htmlHelper)
        {
            RepositorySetting rs = new RepositorySetting();
            return htmlHelper.Raw(string.Format("\"{0}\"", rs["MetaDescription"]));
        }
        public static IHtmlString MetaKeyboard(this HtmlHelper htmlHelper)
        {
            RepositorySetting rs = new RepositorySetting();
            return htmlHelper.Raw(string.Format("\"{0}\"", rs["MetaKeyboard"]));
        }
        public static IHtmlString WriteSlide(this HtmlHelper htmlHelper)
        {
            return new SlideWriter().Write(htmlHelper);
        }
        public static IHtmlString GetbyKeyAndLang(this HtmlHelper htmlhelper, string key, string lang)
        {
            RepositoryLabel repLabel = new RepositoryLabel();
            Label label = repLabel.GetByKey(key);
            if (lang == "tr")
            {
                return htmlhelper.Raw(label.ValueTr);
            }
            else
            {
                return htmlhelper.Raw(label.ValueEn);
            }
        }
        public static string MiddlePath(this HtmlHelper htmlHelper, string picturePath)
        {
            if (string.IsNullOrEmpty(picturePath))
                return "";
            else
                return picturePath.Replace(".main.", ".middle.");
        }
        public static string ThumbPath(this HtmlHelper htmlHelper, string picturePath)
        {
            if (string.IsNullOrEmpty(picturePath))
                return "";
            else
                return picturePath.Replace(".main.", ".thumb.");
        }
        public static IHtmlString Pager(this HtmlHelper htmlHelper,
        PaginatedList pagingData, string queryData, Func<int?, HtmlHelper, string, string> fun)
        {
            StringBuilder html = new StringBuilder("");
            html.Append("<div class=\"wp-pagenavi\">");
            if (pagingData.TotalPages > 1)
            {


                // Previous Page

                if (pagingData.PageIndex > 1)
                {
                    if (pagingData.PageIndex == 2)
                    {

                        html.AppendFormat("<a href=\"{0}\" class=\"nextpostslink\" >", fun(null, htmlHelper, queryData));
                        html.Append(" << önceki </a>");

                    }
                    else
                    {
                        html.AppendFormat("<a href=\"{0}\" class=\"nextpostslink\" >", fun(pagingData.PageIndex - 1, htmlHelper, queryData));
                        html.Append(" << önceki </a>");
                    }

                }
                else
                {
                    //html.Append("<span class=\"last\" > << önceki</span>");
                }

                // first page
                if (pagingData.PageIndex > 1)
                {
                    html.AppendFormat("<a href=\"{0}\" class=\"page larger\"  >", fun(null, htmlHelper, queryData));
                    html.Append("1</a>");
                }
                if (pagingData.PageIndex > 4)
                    html.Append("<span class=\"extend\">...</span>");
                // current page previous 2
                for (int i = pagingData.PageIndex - 2; i < pagingData.PageIndex; i++)
                {
                    if (i > 1)
                    {
                        html.AppendFormat("<a href=\"{0}\" class=\"page larger\"  >{1}", fun(i, htmlHelper, queryData), i);
                        html.Append("</a>");
                    }
                }
                // current page
                if (pagingData.PageIndex != pagingData.TotalPages)
                    html.AppendFormat("<span class=\"current\">{0}</span>", pagingData.PageIndex);


                // current page next 2
                for (int i = pagingData.PageIndex + 1; i < pagingData.PageIndex + 3; i++)
                {
                    if (i < pagingData.TotalPages)
                    {
                        html.AppendFormat("<a href=\"{0}\" class=\"page larger\"  >{1}", fun(i, htmlHelper, queryData), i);
                        html.Append("</a>");
                    }
                }
                if ((pagingData.PageIndex + 2) < pagingData.TotalPages - 1)
                    html.Append("<span class=\"extend\">...</span>");
                // last Page
                if (pagingData.PageIndex == pagingData.TotalPages)
                {
                    html.AppendFormat("<span class=\"current\">{0}</span>", pagingData.PageIndex);

                }
                else
                {
                    html.AppendFormat("<a href=\"{0}\" class=\"page larger\"  >{1}", fun(pagingData.TotalPages, htmlHelper, queryData), pagingData.TotalPages);
                }
                // Next Page
                if (pagingData.PageIndex < pagingData.TotalPages)
                {

                    html.AppendFormat("<a href=\"{0}\" class=\"nextpostslink\" >", fun(pagingData.PageIndex + 1, htmlHelper, queryData));

                    html.Append("sonraki >> </a>");
                }
                else
                {

                    //html.Append("<span class=\"AtEnd\" > sonraki >> </span>");
                }

                html.Append("</div>");
                html.Append("<br style=\"clear:both\" />");
            }
            return htmlHelper.Raw(html.ToString());

        }
        public static IHtmlString WriteNav(this HtmlHelper helper, string lang) {
            NavWriter wr = new NavWriter(helper,lang);
            return wr.Write();
        }
    }
    
}