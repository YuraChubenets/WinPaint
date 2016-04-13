using System;
using System.Collections.Generic;
using System.Drawing;
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
        string GetImagePath { get; set; }
    }
}