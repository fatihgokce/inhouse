using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Inhouse.Services
{
    public class ImageHelper
    {
        //public Guid FileName { get; set; }
        public string FileName { get; set; }
        public static string GetExtendedName(string path, ImageNameExtension imageNameExtension)
        {
            string extension = Enum.GetName(typeof(ImageNameExtension), imageNameExtension).ToLower();
            return string.Format("{0}.{1}{2}",
                Path.Combine(
                    Path.GetDirectoryName(path),
                    Path.GetFileNameWithoutExtension(path)),
                extension,
                Path.GetExtension(path));


        }

        public string FileNameAsString
        {
            get
            {
                return this.FileName;
                //if (this.FileName.Contains(".jpg"))
                //    return this.FileName;
                //else
                //    return this.FileName.ToString() + ".jpg";
            }
        }
        public string MiniFileName
        {
            get
            {
                return GetExtendedName(this.FileNameAsString, ImageNameExtension.Mini);
            }
        }
        public string ThumbFileName
        {
            get
            {
                return GetExtendedName(this.FileNameAsString, ImageNameExtension.Thumb);
            }
        }

        public string MainFileName
        {
            get
            {
                return GetExtendedName(this.FileNameAsString, ImageNameExtension.Main);
            }
        }
        public string MiddleFileName
        {
            get
            {
                return GetExtendedName(this.FileNameAsString, ImageNameExtension.Middle);
            }
        }
    }
}