using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPaint.BL
{
    public interface IWinPaint<T>
    {
        void SetImage(T img);
        string ImagePath { get; set; }
        Item CurrentItem { get; set; }
        T ContentImage { get; set; }
        void SetContorlEnable(bool flag);
        void InitialSetings();

        void ShowProgress(int progressPercentage);

        event EventHandler ImageOpenClick;
        event EventHandler ImageSaveClick;
        event EventHandler ImageInvert;
        event EventHandler ImageGrayscale;
        event EventHandler ImageNewClick;
    }
}
