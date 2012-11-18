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
    public class RepositorySpot : BaseRepository, IRepository<Spot>
    {
        #region IRepository<Spot> Members

        public void Insert(Spot item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Spot](Path,Active)");
            sb.AppendFormat("values('{0}','{1}')",
                item.Path, item.Active);
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Spot item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("update [Spot] set Path='{0}',Active='{1}' where SpotId={2}",
                item.Path, item.Active, item.SpotId);
            ExecuteNonQuery(sb.ToString());
        }

        public Spot GetById(long id)
        {
            string sql = string.Format("select * from [Spot] where SpotId={0}", id);
            DataTable dt = GetDataTable(sql);
            var entity = GetEntity<Spot>(dt.Rows[0]);
            return entity;
        }

        public List<Spot> GetAll()
        {
            string sql = "select * from [Spot]";

            DataTable dt = GetDataTable(sql);
            List<Spot> liste = new List<Spot>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Spot>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        public void Delete(long id)
        {
            ExecuteNonQuery("delete from [Spot] where SpotId={0}".With(id));
        }

        #endregion
    }
}