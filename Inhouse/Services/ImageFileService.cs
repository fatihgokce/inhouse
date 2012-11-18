using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Inhouse.Services
{
    public class ImageFileService
    {
        private readonly string imageFolder;

        public ImageFileService(string imageFolder)
        {
            this.imageFolder = imageFolder;
        }
        public ImageFileService()
            : this("/upload/")
        {
        }
        public virtual string GetImageFolderPath()
        {
            return HttpContext.Current.Server.MapPath(imageFolder);
        }
        public string GetFullPath(string filename)
        {
            return Path.Combine(GetImageFolderPath(), filename);
        }
        public string GetThumbPath(string image)
        {
            return GetFullPath(image);
        }
        public string GetThumbPath(ImageHelper image)
        {
            return GetFullPath(image.ThumbFileName);
        }
        public string GetMainPath(string image)
        {
            return GetFullPath(image);
        }
        public string GetMainPath(ImageHelper image)
        {
            return GetFullPath(image.MainFileName);
        }
        public string GetMiddlePath(ImageHelper image)
        {
            return GetFullPath(image.MiddleFileName);
        }
        public string GetMiddlePath(string image)
        {
            return GetFullPath(image);
        }
        public string GetRelativeUrl(string filename)
        {
            return string.Format("upload/{0}", filename);
        }
    }
}