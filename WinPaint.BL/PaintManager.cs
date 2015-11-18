using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPaint.BL
{

    public interface IPaintManager
    {
        bool IsExist(string imagePath);
        Image GetImage(string imagePath);      
        void SaveImage(Image img);
        string GetCurrentPath { get; }
        string GetImagePath { get; set; }

        Image GetGrayscale(int i, int j, Image image);
        Image GetInvert(int i, int j, Image image);
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

        public Image GetGrayscale(int i, int j, Image image)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;

            c = bmap.GetPixel(i, j);
            byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

            bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));

            image = (Bitmap)bmap.Clone();
            return image;
        }

        public Image GetInvert(int i, int j, Image image)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;

            c = bmap.GetPixel(i, j);
            bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));

            image = (Bitmap)bmap.Clone();
            return image;
        }
        

        public bool IsExist(string imagePath)
        {
              return    File.Exists(imagePath);        
        }


        public Image GetImage(string imagePath)
        {
            return Bitmap.FromFile(imagePath);
        }

        public void SaveImage(Image img)
        {
            string path = GetImagePath ??  GetCurrentPath;
            img.Save(path, ImageFormat.Bmp);           
        }


        //void floodFill4(int x, int y, int newColor, int oldColor)
        //{
        //    if (x >= 0 && x < w && y >= 0 && y < h && screenBuffer[x][y] == oldColor && screenBuffer[x][y] != newColor)
        //    {
        //        screenBuffer[x][y] = newColor; //set color before starting recursion

        //        floodFill4(x + 1, y, newColor, oldColor);
        //        floodFill4(x - 1, y, newColor, oldColor);
        //        floodFill4(x, y + 1, newColor, oldColor);
        //        floodFill4(x, y - 1, newColor, oldColor);
        //    }
        //}

    }
}
