using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Models
{
    public class Proje
    {
        public long ProjeId { get; set; }
        public string ProjeAdiTr { get; set; }
        public string ProjeAdiEn { get; set; }
        public string Il { get; set; }
        public string Ilce { get; set; }
        public string AciklamaTr { get; set; }
        public string AciklamaEn { get; set; }
        public string AraziAlani { get; set; }
        public string VaziyetPlaniTr { get; set; }
        public string VaziyetPlaniEn { get; set; }
        public string BlokSayisi { get; set; }
        public string DaireSayisi { get; set; }
        public string InsaatAlani { get; set; }
        public string FiyatAralik { get; set; }
        public string SosyalEtkinlikTr { get; set; }
        public string SosyalEtkinlikEn { get; set; }
        public string LokasyonTr { get; set; }
        public string LokasyonEn { get; set; }
        public string UlasimOzellikTr { get; set; }
        public string UlasimOzellikEn { get; set; }
        public string KatDairePlani { get; set; }
        public string HaritaScript { get; set; }
        public DateTime Tarih { get; set; }
        public string PicturePath1 { get; set; }
        public string PicturePath2 { get; set; }
        public string PicturePath3 { get; set; } 
        public string PicturePath4 { get; set; }
        public string PicturePath5 { get; set; }
        public string PicturePath6 { get; set; }

    }
}