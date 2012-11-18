using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using Inhouse.Models;
namespace Inhouse.Repositorys
{
    public class RepositoryMessage : BaseRepository, IRepository<Message>
    {
        public void Insert(Message item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat("insert into [Message](NameSurname,Mail,Phone,Mesaj,AddedDate)");
            sb.AppendFormat("values('{0}','{1}','{2}','{3}','{4}')",
                item.NameSurname, item.Mail, item.Phone,item.Mesaj, item.AddedDate.ToString("yyyy-MM-dd HH:mm:ss"));
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Message item)
        {
            throw new NotImplementedException();
        }

        public Message GetById(long id)
        {
            string sql = string.Format("select * from [Message] where ContactId='{0}'", id);

            DataTable dt = GetDataTable(sql);
            Message entity = null;
            if (dt.Rows.Count > 0)
                entity = GetEntity<Message>(dt.Rows[0]);
            return entity;
        }

        public List<Message> GetAll()
        {
            string sql = "select * from [Message]";

            DataTable dt = GetDataTable(sql);
            List<Message> liste = new List<Message>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Message>(dr);
                liste.Add(entity);
            }
            return liste;
        }
    }
}