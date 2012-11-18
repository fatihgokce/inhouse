using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Inhouse.Services
{
    public class ImageService
    {
        ImageSize mainSize;
        ImageSize middleSize;
        ImageSize thumbnailSize;
        ImageSize miniSize;
        ImageFileService imageFileService;

        public ImageService(int mainHeight, int mainWidth, int middleHeight, int middleWidth, int thumbHeight, int thumbWidth
            , int miniHeight, int miniWidth)
        {
            this.imageFileService = new ImageFileService();
            this.mainSize = new ImageSize(mainWidth, mainHeight);
            this.thumbnailSize = new ImageSize(thumbWidth, thumbHeight);
            this.middleSize = new ImageSize(middleWidth, middleHeight);
            this.miniSize = new ImageSize(miniHeight, miniWidth);
        }
        public ImageService(int height, int width)
            : this(height, width, 0, 0, 0, 0, 0, 0)
        {

        }
        public ImageService()
            : this(268,458, 152, 220, 89, 140, 40, 40)
        { }

        /// <summary>
        /// Creates a main,middle and thumnail image from the given image of the taget size
        /// </summary>
        /// <param name="imagePath"></param>
        public void CreateSizedImages(ImageHelper image, bool createMiddle, bool createThumb, bool createMini)
        {
            string imagePath = imageFileService.GetFullPath(image.FileNameAsString);
            using (System.Drawing.Image gdiImage = System.Drawing.Image.FromFile(imagePath))
            {
                CreateSizedImage(gdiImage, mainSize, ImageHelper.GetExtendedName(imagePath, ImageNameExtension.Main));
                if (createMiddle)
                    CreateSizedImage(gdiImage, middleSize, ImageHelper.GetExtendedName(imagePath, ImageNameExtension.Middle));
                if (createThumb)
                    CreateSizedImage(gdiImage, thumbnailSize, ImageHelper.GetExtendedName(imagePath, ImageNameExtension.Thumb));
                if (createMini)
                    CreateSizedImage(gdiImage, miniSize, ImageHelper.GetExtendedName(imagePath, ImageNameExtension.Mini));
            }
        }

        private void CreateSizedImage(System.Drawing.Image image, ImageSize targetSize, string targetPath)
        {
            ImageComparer imageComparer = new ImageComparer(image, targetSize);
            ImageSize newImageSize = targetSize;

            //if (imageComparer.IsLandscape)
            //{
            //    newImageSize = imageComparer.LandscapeSize;
            //}
            //else
            //{
            //    newImageSize = imageComparer.PortraitSize;
            //}

            //using (Bitmap bitmap = new Bitmap(newImageSize.Width, newImageSize.Height,
            //    System.Drawing.Imaging.PixelFormat.Format32bppRgb))
            using (Bitmap bitmap = new Bitmap(newImageSize.Width, newImageSize.Height))
            {
                bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    //graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                    graphics.DrawImage(image,
                        new Rectangle(0, 0, newImageSize.Width, newImageSize.Height),
                        new Rectangle(0, 0, image.Width, image.Height),
                        GraphicsUnit.Pixel);
                }
                bitmap.Save(targetPath);
                //bitmap.Save(targetPath, ImageFormat.Jpeg);
            }
        }
        ImageFormat GetImageFormat(string path)
        {
            string ext = Path.GetExtension(path);
            if (ext == "jpg" || ext == "jpeg")
                return ImageFormat.Jpeg;
            else if (ext == "png")
                return ImageFormat.Png;
            return ImageFormat.Jpeg;
        }
    }
}