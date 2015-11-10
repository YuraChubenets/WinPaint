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
        void SaveImage(Image img, string filename);
        string GetCurrentPath();

        Image GetGrayscale(int i, int j, Image image);
        Image GetInvert(int i, int j, Image image);
    }

    public   class PaintManager : IPaintManager
    {
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

        public Image GetInvert(int i, int j , Image image)
        {
            Bitmap temp = (Bitmap)image;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
           
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j,Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
              
           image = (Bitmap)bmap.Clone();
           return image;
        }

        public string GetCurrentPath()
        {
            return System.IO.Directory.GetCurrentDirectory();
        }
    
        public bool IsExist(string imagePath)
        {
            bool flag = File.Exists(imagePath);
            return flag;
        }

        
        public Image GetImage( string imagePath)
        {            
            return Bitmap.FromFile(imagePath);
        }   

        public void SaveImage(Image img, string fileName)
        {
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            img.Save(fileName, ImageFormat.Bmp);
        }     
    }    
}
