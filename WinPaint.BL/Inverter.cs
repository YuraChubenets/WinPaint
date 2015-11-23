using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinPaint.BL
{
  public  class Inverter
    {

        private bool _cancell = false;
        private Image _image;
        public void Cancel()
        {
            _cancell = true;
        }

        public Inverter(Image image)
        {
            _image = image;
        }

        public bool Work()
        {
            Bitmap bmap = (Bitmap)_image.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (_cancell)
                        break;
                    try {
                        c = bmap.GetPixel(i, j);
                        bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                    }
                    catch { }
                }
                Thread.Sleep(1);
                OnProgressChanged((Bitmap)bmap.Clone());
            }
            
            bmap.Dispose();
            return _cancell;
        }

        public void OnProgressChanged( Image image)
        {
            if (ProcessChanged != null)
                ProcessChanged(image);
        }
        public event Func<Image,Image> ProcessChanged;
    }
}
