using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Inhouse.Models;

namespace Inhouse.Repositorys
{
    public class RepositoryContact : BaseRepository, IRepository<Contact>
    {
        public void Insert(Contact item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Contact](NameSurname,Mail,Phone,AddedDate)");
            sb.AppendFormat("values('{0}','{1}','{2}','{3}')",
                item.NameSurname, item.Mail, item.Phone, item.AddedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Contact item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");

            sb.AppendFormat("update [Conatct] set NameSurname='{0}',Mail='{1}',Phone='{2}'  where ContactId={3}",
                item.NameSurname, item.Mail, item.Phone,item.ContactId);
            ExecuteNonQuery(sb.ToString());
        }

        public Contact GetById(long id)
        {
            string sql = string.Format("select * from [Contact] where ContactId='{0}'", id);

            DataTable dt = GetDataTable(sql);
            Contact entity = null;
            if (dt.Rows.Count > 0)
                entity = GetEntity<Contact>(dt.Rows[0]);
            return entity;
        }

        public List<Contact> GetAll()
        {
            string sql = "select * from [Contact]";

            DataTable dt = GetDataTable(sql);
            List<Contact> liste = new List<Contact>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Contact>(dr);
                liste.Add(entity);
            }
            return liste;
        }
    }
}