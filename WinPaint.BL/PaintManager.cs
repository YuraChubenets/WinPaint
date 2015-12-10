using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace WinPaint.BL
{

    public interface IPaintManager
    {
        bool IsExist(string imagePath);
        Image GetImage(string imagePath);
        void SaveImage(Image img);
        string GetCurrentPath { get; }
        string GetImagePath { get; set; }
    }



    public class PaintManager : IPaintManager
    {
       
        public string GetCurrentPath
        {
            get
            {
                return Directory.GetCurrentDirectory();
            }

        }
        public string GetImagePath { get; set; }
       

        public Image GetImage(string imagePath)
        {
            return (Image)Image.FromFile(imagePath).Clone();
        }

      
        

        public bool IsExist(string imagePath)
        {
              return  File.Exists(imagePath);        
        }       
        public void SaveImage(Image img)
        {
            string path = GetImagePath ??  GetCurrentPath;
            img.Save(path, ImageFormat.Bmp);           
        }
    }
}
