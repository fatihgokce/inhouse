using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Inhouse.Models;
using Inhouse.Extensions;
namespace Inhouse.Repositorys
{
    public class RepositoryPage : BaseRepository, IRepository<Page>
    {
        #region IRepository<Page> Members
        public void Insert(Page item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Page](PageNameTr,PageNameEn,Position,ContextTr,ContextEn,Active,ParentId)");
            sb.AppendFormat("values('{0}','{1}',{2},'{3}','{4}','{5}',{6})",
                item.PageNameTr,item.PageNameEn, item.Position, item.ContextTr,item.ContextEn, item.Active, item.ParentId);
            ExecuteNonQuery(sb.ToString());
        }
        public void Update(Page item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("update [Page] set PageNameTr='{0}',PageNameEn='{1}',Position={2},ContextTr='{3}',ContextEn='{4}',Active='{5}',ParentId={6} where PageId={7}",
                item.PageNameTr,item.PageNameEn, item.Position, item.ContextTr,item.ContextEn, item.Active, item.ParentId, item.PageId);
            ExecuteNonQuery(sb.ToString());
        }
        public Page GetById(long id)
        {
            string sql = string.Format("select * from [Page] where PageId={0}", id);
            DataTable dt = GetDataTable(sql);
            Page entity = GetEntity<Page>(dt.Rows[0]);
            return entity;
        }
        // return Session.CreateQuery("from Category cat where cat.Id!=:pid 
        //and cat.ParentCategory.Id =:cid order by cat.Position asc")
        //         .SetInt32("pid", GetRootCategory().Id).SetInt32("cid", category.Id).List<Category>() as List<Category>;
        public List<Page> GetParentSubCategory(Page page)
        {
            string sql = string.Format(@"select * from [Page] 
where ParentId={0}  order by Position asc", page.PageId);
            DataTable dt = GetDataTable(sql);
            List<Page> liste = new List<Page>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Page entity = GetEntity<Page>(dr);
                liste.Add(entity);
            }
            return liste;
            //return Session.CreateQuery("from Category cat where cat.Id!=:pid and cat.ParentCategory.Id =:cid order by cat.Position asc")
            //       .SetInt32("pid", GetRootCategory().Id).SetInt32("cid", category.Id).List<Category>() as List<Category>;

        }
        public List<Page> GetChilds(int pageId)
        {
            string sql = string.Format("select * from [Page] where ParentId={0}  order by Position asc", pageId);
            DataTable dt = GetDataTable(sql);
            List<Page> liste = new List<Page>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Page entity = GetEntity<Page>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        public List<Page> GetAll()
        {
            string sql = "select * from [Page] order by Position asc";

            DataTable dt = GetDataTable(sql);
            List<Page> liste = new List<Page>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Page entity = GetEntity<Page>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        public void Delete(long id)
        {
            ExecuteNonQuery("delete from [Page] where PageId={0}".With(id));
        }
        #endregion
        public List<Page> GetChildPages(long parentId)
        {
            string sql = string.Format(@"
select * from Page where ParentId={0} order by Position asc", parentId);
            DataTable dt = GetDataTable(sql);
            List<Page> liste = new List<Page>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Page entity = GetEntity<Page>(dr);
                liste.Add(entity);
            }
            return liste;
            //return Session.QueryOver<Category>().
            //Where(x => x.ParentCategory.Id == parentId).OrderBy(x => x.Position).Asc.List() as List<Category>;
        }
        public Page GetRootPage()
        {
            string sql = string.Format(@"
select * from Page where PageNameTr='{0}'", Page.RootPageName);
            DataTable dt = GetDataTable(sql);
            Page entity = new Page();
            if (dt.Rows.Count > 0)
                entity = GetEntity<Page>(dt.Rows[0]);
            return entity;

        }

        public Page GetItemBefore(Page page)
        {//
            Page pg = null;
            string sql = string.Format(@"
select p1.* from Page p1
where p1.[ParentId]={0}
and p1.[Position]< {1} ", page.ParentId, page.Position);
            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count == 0)
                return default(Page);
            sql = string.Format(@"
            select max(p1.Position) from Page p1
            where p1.[ParentId]={0}
            and p1.[Position]< {1}  ", page.ParentId, page.Position);
            dt = GetDataTable(sql);
            long max_parent_id = long.Parse(dt.Rows[0][0].ToString());

            sql = string.Format(@"
            select p1.* from Page p1
where p1.[ParentId]={0}
and p1.[Position]< {1} and p1.Position={2}", page.ParentId, page.Position, max_parent_id);
            dt = GetDataTable(sql);

            if (dt.Rows.Count > 0)
            {
                pg = GetEntity<Page>(dt.Rows[0]);
            }
            return pg;
        }
        public Page GetItemAfter(Page page)
        {
            Page pg = null;
            string sql = string.Format(@"
select p1.* from Page p1
where p1.[ParentId]={0}
and p1.[Position]> {1} ", page.ParentId, page.Position);
            DataTable dt = GetDataTable(sql);
            if (dt.Rows.Count == 0)
                return default(Page);
            sql = string.Format(@"
            select min(p1.Position) from Page p1
            where p1.[ParentId]={0}
            and p1.[Position]> {1}  ", page.ParentId, page.Position);
            dt = GetDataTable(sql);
            long max_parent_id = long.Parse(dt.Rows[0][0].ToString());

            sql = string.Format(@"
            select p1.* from Page p1
where p1.[ParentId]={0}
and p1.[Position]> {1} and p1.Position={2}", page.ParentId, page.Position, max_parent_id);
            dt = GetDataTable(sql);

            if (dt.Rows.Count > 0)
            {
                pg = GetEntity<Page>(dt.Rows[0]);
            }
            return pg;
        }
        public Page GetByPageName(string pageName)
        {
            string sql = string.Format(@"
            select p1.* from Page p1
where p1.[PageName]={0}", pageName);
            var dt = GetDataTable(sql);
            Page pg = null;
            if (dt.Rows.Count > 0)
            {
                pg = GetEntity<Page>(dt.Rows[0]);
            }
            return pg;
        }
        public int GetChildMaxPosition(long parentId)
        {
            string sql = string.Format("select max(Position) from Page where ParentId={0}", parentId);
            DataTable dt = GetDataTable(sql);
            int a = 1;
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
                a = int.Parse(dt.Rows[0][0].ToString());
            return a;
        }
        public void SwapPosition(long pageId, PagePosition page_position)
        {
            Page currentPage = GetById(pageId);
            Page swapPage = default(Page);
            if (PagePosition.Up == page_position)
                swapPage = GetItemBefore(currentPage);
            else
                swapPage = GetItemAfter(currentPage);
            if (swapPage != null && currentPage != null)
            {
                int tmpPosition = currentPage.Position;
                currentPage.Position = swapPage.Position;
                swapPage.Position = tmpPosition;
                Update(currentPage);
                Update(swapPage);

            }
        }



    }
}