using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inhouse.Models;
using System.Text;
using System.Data;
namespace Inhouse.Repositorys
{
    //public class RepositoryUser : BaseRepository, IRepository<User>
    //{
    //    #region IRepository<User> Members

    //    public void Insert(User item)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("PRAGMA journal_mode = OFF;");
    //        sb.AppendFormat("insert into [User](UserName,Password)");
    //        sb.AppendFormat("values('{0}','{1}')",
    //            item.UserName,item.Password);
    //        ExecuteNonQuery(sb.ToString());
    //    }

    //    public void Update(User item)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append("PRAGMA journal_mode = OFF;");
    //        sb.AppendFormat("update [User] set UserName='{0}',Password='{1}' where UserId={2}",item.UserName,item.Password,item.UserId);           
    //        ExecuteNonQuery(sb.ToString());
    //    }

    //    public User GetById(long id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public List<User> GetAll()
    //    {
        
    //        string sql = "select * from [User]";
          
    //        DataTable dt = GetDataTable(sql);
    //        List<User> liste = new List<User>();
    //        for (int i = 0; i < dt.Rows.Count; i++)
    //        {
    //            DataRow dr = dt.Rows[i];
    //            User entity = GetEntity<User>(dr);
    //            liste.Add(entity);
    //        }
    //        return liste;
    //    }

    //    #endregion
    //    public bool IsUser(string user_name,string password)
    //    {
    //        string sql = string.Format("select * from [User] where UserName='{0}' and Password='{1}'", user_name,password);          
    //        DataTable dt = GetDataTable(sql);
    //        if (dt.Rows.Count > 0)
    //            return true;
    //        else
    //            return false;
            
    //    }
    //    public void Delete(long id)
    //    {
    //        ExecuteNonQuery("delete from [User] where UserId={0}".With(id));
    //    }
    //}
}