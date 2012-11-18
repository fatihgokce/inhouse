using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Inhouse.Repositorys;
using Inhouse.Extensions;
namespace Inhouse.Writers
{
    public class SlideWriter
    {
        public IHtmlString Write(HtmlHelper html_helper)
        {
            var list = new RepositorySpot().GetAll();
            StringBuilder html = new StringBuilder();
            html.Append("<div id='featured' class='clearfix'>");
            html.Append("<a href='#' id='left_arrow'>Previous</a>");
            html.Append("<a href='#' id='right_arrow'>Next</a>");
            html.Append("<div id='featured_content'> ");
            foreach (var item in list)
            {
                html.Append("<div class='slide'>");
                html.Append("<img src='{0}' alt='' width='960px' heihgt='333px' style='height:333px !important' />".With(item.Path));
                html.Append("</div>");
            }
            html.Append(@"</div>
 <div id='controllers' class='clearfix'></div>                 
</div>");
            return html_helper.Raw(html.ToString());
        }
    }
}