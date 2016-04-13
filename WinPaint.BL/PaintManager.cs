using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace WinPaint.BL
{
        
    public class PaintManager : IPaintManager
    {
        private string getImagePath;    
        public string GetImagePath
        {
            get {
                if (getImagePath != null)
                    getImagePath = Directory.GetCurrentDirectory();
                 return getImagePath;
                }
            set { getImagePath = value; }
        }       
        public Image  GetImage(string imagePath)
        {
            return (Image)Image.FromFile(imagePath).Clone();
        }        
        public bool   IsExist(string imagePath)
        {
            return File.Exists(imagePath);
        }
        public void   SaveImage(Image img)
        {
             img.Save(GetImagePath, ImageFormat.Bmp);
        }
    }
}
