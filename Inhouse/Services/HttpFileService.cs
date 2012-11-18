using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Inhouse.Services
{
    public class HttpFileService
    {
        ImageFileService imageFileService;
        ImageService imageService;
        string _imgId;
        string imageName;
        public HttpFileService(string imgId)
        {
            this.imageFileService = new ImageFileService();
            this.imageService = new ImageService();
            _imgId = imgId;
            imageName = imgId;
        }
        public HttpFileService(string imgId, ImageService imgSer)
        {
            this.imageFileService = new ImageFileService();
            this.imageService = imgSer;
            _imgId = imgId;
            imageName = imgId;
        }

        public IEnumerable<ImageHelper> GetUploadedImages(HttpRequestBase request, bool createMiddle, bool createThumb, bool createMini)
        {
            List<ImageHelper> images = new List<ImageHelper>();

            foreach (string inputTagName in request.Files)
            {
                HttpPostedFileBase file = request.Files[inputTagName];
                if (file.ContentLength > 0)
                {
                    // upload the image to filesystem
                    //if (IsNotImage(file))
                    //{
                    //    throw new Exception(string.Format("File '{0}' is not an image file (*.jpg)", file.FileName));
                    //}

                    ImageHelper image = new ImageHelper
                    {
                        FileName = imageName
                        //Description = Path.GetFileName(file.FileName)
                    };
                    if (string.IsNullOrEmpty(imageName))
                    {
                        image.FileName = file.FileName;
                    }
                    else
                    {
                        image.FileName = string.Format("{0}{1}", imageName, Path.GetExtension(file.FileName));
                    }
                    if (File.Exists(imageFileService.GetFullPath(image.FileNameAsString)))
                        File.Delete(imageFileService.GetFullPath(image.FileNameAsString));

                    file.SaveAs(imageFileService.GetFullPath(image.FileNameAsString));

                    // convert the image to main and thumb sizes
                    imageService.CreateSizedImages(image, createMiddle, createThumb, createMini);

                    File.Delete(imageFileService.GetFullPath(image.FileNameAsString));

                    images.Add(image);
                }
            }

            return images;
        }

        private bool IsNotImage(HttpPostedFileBase file)
        {
            string extension = Path.GetExtension(file.FileName).ToLower();
            return (extension != ".jpg");
        }
    }
}