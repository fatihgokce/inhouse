using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Inhouse.Models;

namespace Inhouse.Repositorys
{
    public class RepositoryCareer : BaseRepository, IRepository<Career>
    {
        public void Insert(Career item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Career](NameSurname,Mail,Phone,CvPath,AddedDate,Message)");
            sb.AppendFormat("values('{0}','{1}','{2}','{3}','{4}','{5}')",
                item.NameSurname, item.Mail, item.Phone,item.CvPath,item.AddedDate.ToString("yyyy-MM-dd HH:mm:ss"),item.Message);
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Career item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");

            sb.AppendFormat("update [Career] set NameSurname='{0}',Mail='{1}',Phone='{2}',CvPath='{3}'  where CareerId={4}",
                item.NameSurname, item.Mail, item.Phone, item.CvPath,item.CareerId);
            ExecuteNonQuery(sb.ToString());
        }

        public Career GetById(long id)
        {
            string sql = string.Format("select * from [Career] where CareerId='{0}'", id);

            DataTable dt = GetDataTable(sql);
            Career entity = null;
            if (dt.Rows.Count > 0)
                entity = GetEntity<Career>(dt.Rows[0]);
            return entity;
        }

        public List<Career> GetAll()
        {
            string sql = "select * from [Career]";

            DataTable dt = GetDataTable(sql);
            List<Career> liste = new List<Career>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Career>(dr);
                liste.Add(entity);
            }
            return liste;
        }
    }
}