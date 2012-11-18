using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inhouse.Models;
using System.Data;
using System.Text;
using Inhouse.Extensions;
namespace Inhouse.Repositorys
{
	public class RepositoryLabel:BaseRepository,IRepository<Label>
	{
        #region IRepository<Label> Members

        public void Insert(Label item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Label](Key,ValueTr,ValueEn)");
            sb.AppendFormat("values('{0}','{1}','{2}')",
                item.Key,item.ValueTr,item.ValueEn);
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Label item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            string conStr = "";
           
            sb.AppendFormat("update [Label] set ValueEn='{0}',ValueTr='{1}'  where Key='{2}'",
                item.ValueEn,item.ValueTr,item.Key);
            ExecuteNonQuery(sb.ToString());
        }

        public Label GetById(long id)
        {
            string sql = string.Format("select * from [Label] where LabelId='{0}'", id);

            DataTable dt = GetDataTable(sql);
             Label entity =null;
            if(dt.Rows.Count>0)
                entity = GetEntity<Label>(dt.Rows[0]);
            return entity;
        }
        public List<Label> GetAll()
        {
            string sql = "select * from [Label]";

            DataTable dt = GetDataTable(sql);
            List<Label> liste = new List<Label>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                Label entity = GetEntity<Label>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        #endregion
        public Label GetByKey(string key)
        {
            string sql = string.Format("select * from [Label] where Key='{0}'", key);

            DataTable dt = GetDataTable(sql);
            Label entity = new Label() ;
            if (dt.Rows.Count > 0)
                entity = GetEntity<Label>(dt.Rows[0]);
            return entity;
        } 
        public void Delete(string key)
        {
            ExecuteNonQuery("delete from [Label] where Key='{0}'".With(key));
        }

    }
}