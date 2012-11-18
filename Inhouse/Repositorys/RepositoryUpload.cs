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
   public class RepositoryUpload : BaseRepository, IRepository<Upload>
    {
        #region IRepository<Page> Members

        public void Insert(Upload item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Upload](Path)");
            sb.AppendFormat("values('{0}')",
                item.Path);
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Upload item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("update [Upload] set Path='{0}' where UploadId={1}",
                item.Path, item.UploadId);
            ExecuteNonQuery(sb.ToString());
        }

        public Upload GetById(long id)
        {
            string sql = string.Format("select * from [Upload] where UploadId={0}", id);
            DataTable dt = GetDataTable(sql);
            var entity = GetEntity<Upload>(dt.Rows[0]);
            return entity;
        }

        public List<Upload> GetAll()
        {
            string sql = "select * from [Upload]";

            DataTable dt = GetDataTable(sql);
            List<Upload> liste = new List<Upload>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Upload>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        public void Delete(long id)
        {
            ExecuteNonQuery("delete from [Upload] where UploadId={0}".With(id));
        }
        #endregion
    }
}
