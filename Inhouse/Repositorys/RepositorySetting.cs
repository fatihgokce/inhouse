using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inhouse.Models;
using Inhouse.Extensions;
namespace Inhouse.Repositorys
{
    public struct SettingKey
    {
        public const string SiteName = "SiteName";
        public const string PagerCount = "PagerCount";
    }
    public class RepositorySetting : BaseRepository, IRepository<Setting>
    {
        #region IRepository<Setting> Members

        public void Insert(Setting item)
        {
            throw new NotImplementedException();
        }

        public void Update(Setting item)
        {
            throw new NotImplementedException();
        }

        public Setting GetById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Setting> GetAll()
        {
            throw new NotImplementedException();
        }

        #endregion
        public string this[string key]
        {
            get
            {
                object val = ExecuteScalar("select Value from Setting where Key='{0}' ".With(key));
                if (val != null)
                    return val.ToString();
                else
                    return string.Empty;
            }
            set
            {
                object val = ExecuteScalar("select Value from Setting where Key='{0}' ".With(key));
                if (val != null)
                {
                    if (value!=null && val.ToString().Trim() != value.Trim())
                    {
                        string sql = "PRAGMA journal_mode = OFF;update Setting set Value='{0}' where Key='{1}'".With(value, key);
                        ExecuteNonQuery(sql);
                    }
                }
                else
                {
                    string sql = "PRAGMA journal_mode = OFF;insert into Setting(Key,Value) values('{0}','{1}')".With(key, value);
                    ExecuteNonQuery(sql);
                }
            }
        }
    }
}