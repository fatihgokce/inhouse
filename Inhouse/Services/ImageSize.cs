using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inhouse.Services
{
    public enum ImageNameExtension
    {
        Mini, Thumb, Middle,
        Main
    }
    public class ImageSize
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public ImageSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }
    }
    public class ImageComparer
    {
        readonly System.Drawing.Image sourceImage;
        readonly ImageSize targetSize;

        public ImageComparer(System.Drawing.Image sourceImage, ImageSize targetSize)
        {
            this.sourceImage = sourceImage;
            this.targetSize = targetSize;
        }

        public float WidthRatio { get { return (float)targetSize.Width / (float)sourceImage.Width; } }
        public float HeightRatio { get { return (float)targetSize.Height / (float)sourceImage.Height; } }
        public bool IsLandscape { get { return HeightRatio >= WidthRatio; } }

        public ImageSize LandscapeSize
        {
            get
            {
                return new ImageSize((int)(sourceImage.Width * WidthRatio), (int)(sourceImage.Height * WidthRatio));
            }
        }

        public ImageSize PortraitSize
        {
            get
            {
                return new ImageSize((int)(sourceImage.Width * HeightRatio), (int)(sourceImage.Height * HeightRatio));
            }
        }
    }
}