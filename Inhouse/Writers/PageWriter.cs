using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Inhouse.Areas.ManagementPanel.Controllers;
using Inhouse.Repositorys;
using Microsoft.Web.Mvc;
using Inhouse.Helpers;
using Inhouse.Extensions;
namespace Inhouse.Writers
{
    public class PageWriter
    {
        readonly Inhouse.Models.Page _rootPage;
        readonly HtmlHelper _htmlHelper;
        readonly RepositoryPage _repPage;
        public PageWriter(Inhouse.Models.Page rootPage, HtmlHelper htmlHelper)
        {
            this._rootPage = rootPage;
            this._htmlHelper = htmlHelper;
            _repPage = new RepositoryPage();
        }
        public string Write()
        {
            var writer = new HtmlTextWriter(new StringWriter());
            writer.RenderBeginTag(HtmlTextWriterTag.Div);
            writer.AddAttribute("id", "categoryList");
            List<Inhouse.Models.Page> childs = _repPage.GetChildPages(_rootPage.PageId);
            WritePages(writer, childs);
            writer.RenderEndTag();
            return writer.InnerWriter.ToString();
        }

        private void WritePages(HtmlTextWriter writer, List<Inhouse.Models.Page> pages)
        {
            bool first = true;
            foreach (var page in pages)
            {
                if (first)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    first = false;
                }

                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.Write(WriteCategory(page));
                List<Inhouse.Models.Page> childs = _repPage.GetChildPages(page.PageId);
                WritePages(writer, childs);
                writer.RenderEndTag();
            }

            if (!first)
                writer.RenderEndTag();
        }

        private string WriteCategory(Inhouse.Models.Page page)
        {

            //return string.Format("{0} {1} {2} {3} {4}",               
            //     _htmlHelper.ActionLink<CategoryController>(c => c.Index(), category.Name),
            //    _htmlHelper.ActionLink<CategoryController>(c => c.Edit(category.Id), "Edit"),
            //    "<a href='#' onclick='DeleteCategory("+category.Id+")'>Sil</a>",
            //    _htmlHelper.UpArrowLink<CategoryController>(c => c.MoveUp(category.Id)),
            //    _htmlHelper.DownArrowLink<CategoryController>(c => c.MoveDown(category.Id))
            //    );
            return string.Format("{0} {1} {2} {3} {4}",
              page.PageNameTr,
              _htmlHelper.ActionLink<PagePanelController>(x => x.UpdatePage(page.PageId), "Güncelle").ToHtmlString(),
               //_htmlHelper.ActionLink<PagePanelController>(x => x.Delete(page.PageId), "Sil").ToHtmlString(), 
               "<a href='javascript:void(0)' onclick='DeletePage(" + page.PageId + ")'>Sil</a>",
              _htmlHelper.UpArrowLink<PagePanelController>(c => c.MoveUp(page.PageId)),
              _htmlHelper.DownArrowLink<PagePanelController>(c => c.MoveDown(page.PageId))
              );
        }

    }
}