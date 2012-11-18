using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Extensions
{
    public static class StringExtension
    {
       
       
        public static string ReplaceIfNotNull(this string txt, string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(txt))
            {
                txt = txt.Replace(oldValue, newValue);
                return txt;
            }
            else
                return "";
        }
        public static string ConvertTrToEn(this string text)
        {
            text = text.Replace('ç', 'c');
            text = text.Replace('Ç', 'C');
            text = text.Replace('ı', 'i');
            text = text.Replace('İ', 'I');
            text = text.Replace('ğ', 'g');
            text = text.Replace('Ğ', 'G');

            text = text.Replace('ş', 's');
            text = text.Replace('Ş', 'S');
            text = text.Replace('ö', 'o');
            text = text.Replace('Ö', 'O');

            text = text.Replace('ü', 'u');
            text = text.Replace('Ü', 'U');
            return text;
        }
        public static string ConvertUtf8ToTr(this string val)
        {
            val = val.Replace("Ã§", "ç");
            val = val.Replace("Ã", "Ç");
            val = val.Replace("Ä", "ğ");
            val = val.Replace("Ä", "Ğ");
            val = val.Replace("Ä±", "ı");
            val = val.Replace("Ä°", "İ");
            val = val.Replace("Ã¶", "ö");
            val = val.Replace("Ã", "Ö");
            val = val.Replace("Å", "ş");
            val = val.Replace("Å", "Ş");
            val = val.Replace("Ã¼", "ü");
            val = val.Replace("Ã", "Ü");
            return val;
        }
        public static string With(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        public static string ConvertWebUrl(this string text)
        {
            text = TrimWithTire(text);
            text = SlashWithTire(text);
            text = ConvertTrToEn(text);
            text = RemoveTirnak(text);
            text = text.Replace("?", "");
            text = text.Replace("@@", "");
            text = text.Replace("%t%", "");
            text = text.Replace(":", "");
            text = text.Replace(".", "");
            text = text.Replace(@"\", "-");
            text = text.Replace("%", "");
            text = text.Replace("&", "");
            text = text.Replace("+", "");
            return text;
        }
        public static string SlashWithTire(this string text)
        {
            text = text.Replace("/", "-");
            return text;
        }
        public static string TrimWithTire(this string text)
        {
            text = text.Replace(" ", "-");
            return text;
        }
        public static string RemoveTirnak(this string str)
        {
            str = str.Replace("\"", "");
            return str;
        }
    }
}