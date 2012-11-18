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
    public class RepositoryProje : BaseRepository, IRepository<Proje>
    {
        public void Insert(Proje item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");
            sb.AppendFormat(@"insert into [Proje](ProjeAdiTr,ProjeAdiEn,Il,Ilce,
            AciklamaTr,AciklamaEn,AraziAlani,VaziyetPlaniTr,VaziyetPlaniEn,
            BlokSayisi,DaireSayisi,InsaatAlani,FiyatAralik,
            SosyalEtkinlikTr,SosyalEtkinlikEn,LokasyonTr,LokasyonEn,UlasimOzellikTr,UlasimOzellikEn,
            KatDairePlani,HaritaScript,Tarih,PicturePath1,PicturePath2,PicturePath3,
            PicturePath4,PicturePath5,PicturePath6)");
            sb.AppendFormat(@"values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',
            '{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',
             '{22}','{23}','{24}','{25}','{26}','{27}')",
                item.ProjeAdiTr, item.ProjeAdiEn, item.Il,item.Ilce,item.AciklamaTr,item.AciklamaEn,
                item.AraziAlani,item.VaziyetPlaniTr,item.VaziyetPlaniEn,item.BlokSayisi,item.DaireSayisi,item.InsaatAlani,
                item.FiyatAralik,item.SosyalEtkinlikTr,item.SosyalEtkinlikEn,item.LokasyonTr,item.LokasyonEn,
                item.UlasimOzellikTr,item.UlasimOzellikEn,item.KatDairePlani,item.HaritaScript,
                item.Tarih.ToString("yyyy-MM-dd HH:mm:ss"),item.PicturePath1,item.PicturePath2,
                item.PicturePath3,item.PicturePath4,item.PicturePath5,item.PicturePath6);
            ExecuteNonQuery(sb.ToString());
        }

        public void Update(Proje item)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PRAGMA journal_mode = OFF;");

            sb.AppendFormat(@"update [Proje] set ProjeAdiTr='{0}',ProjeAdiEn='{1}',Il='{2}',  
            Ilce='{3}',AciklamaTr='{4}',AciklamaEn='{5}',AraziAlani='{6}',VaziyetPlaniTr='{7}',VaziyetPlaniEn='{8}',    
            BlokSayisi='{9}',DaireSayisi='{10}',InsaatAlani='{11}',FiyatAralik='{12}', 
            SosyalEtkinlikTr='{13}',SosyalEtkinlikEn='{14}',LokasyonTr='{15}',LokasyonEn='{16}', 
            UlasimOzellikTr='{17}',UlasimOzellikEn='{18}',KatDairePlani='{19}',HaritaScript='{20}',
            PicturePath1='{21}',PicturePath2='{22}',PicturePath3='{23}',
            PicturePath4='{24}',PicturePath5='{25}',PicturePath6='{26}' where ProjeId={27}",
                item.ProjeAdiTr, item.ProjeAdiEn, item.Il, item.Ilce,item.AciklamaTr,item.AciklamaEn,item.AraziAlani,item.VaziyetPlaniTr,item.VaziyetPlaniEn,
                item.BlokSayisi,item.DaireSayisi,item.InsaatAlani,item.FiyatAralik,item.SosyalEtkinlikTr,
                item.SosyalEtkinlikEn,item.LokasyonTr,item.LokasyonEn,item.UlasimOzellikTr,item.UlasimOzellikEn,
                item.KatDairePlani,item.HaritaScript,item.PicturePath1,
                item.PicturePath2, item.PicturePath3, item.PicturePath4, item.PicturePath5,
                item.PicturePath6, item.ProjeId);
            ExecuteNonQuery(sb.ToString());
        }

        public Proje GetById(long id)
        {
            string sql = string.Format("select * from [Proje] where ProjeId='{0}'", id);

            DataTable dt = GetDataTable(sql);
            Proje entity = null;
            if (dt.Rows.Count > 0)
                entity = GetEntity<Proje>(dt.Rows[0]);
            return entity;
        }

        public List<Proje> GetAll()
        {
            string sql = "select * from [Project]";

            DataTable dt = GetDataTable(sql);
            List<Proje> liste = new List<Proje>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Proje>(dr);
                liste.Add(entity);
            }
            return liste;
        }
        public List<Proje> GetListByDate(int maxRecord, int page, out int totalCount)
        {
            int offset = (page - 1) * maxRecord;
            string sql = "select * from Proje order by Tarih desc limit {0} offset {1}".With(maxRecord, offset);
            //string sql = "select * from Question   order by Date desc limit {0} offset {1}".With(maxRecord, offset);
            object count = ExecuteScalar("select count(*) from Proje");
            totalCount = 0;
            if (count != null)
                totalCount = int.Parse(count.ToString());
            DataTable dt = GetDataTable(sql);
           List<Proje>liste=new List<Proje>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var entity = GetEntity<Proje>(dr); //GetEntity(dr);//GetEntity<Question>(dr);               
               
                liste.Add(entity);
            }
            return liste;
        }
    }
}