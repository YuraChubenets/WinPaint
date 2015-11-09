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
    
        
    }

    public   class PaintManager : IPaintManager
    {
 
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
