using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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

        public bool WorkInvert()
        {
            Bitmap bmap = (Bitmap)_image;

            //ImageAttributes ia = new ImageAttributes();
            //ColorMatrix cm = null;
            //Graphics g = null;

            //   g = Graphics.FromImage(bmap);

            //cm = new ColorMatrix(
            //           new float[][]
            //           {
            //               new float[] {-1, 0, 0, 0, 0},
            //               new float[] {0, -1, 0, 0, 0},
            //               new float[] {0, 0, -1, 0, 0},
            //               new float[] {0, 0, 0, 1, 0},
            //               new float[] {1, 1, 1, 0, 1}
            //           });
            //ia.SetColorMatrix(cm);

            //g.DrawImage(bmap, new Rectangle(0, 0, bmap.Width, bmap.Height),
            //0, 0, bmap.Width, bmap.Height, GraphicsUnit.Pixel, ia);
            //Thread.Sleep(50);
            //OnProgressChanged((Bitmap)bmap.Clone());        

            //if (_image != null)
            //    _image.Dispose();
            //bmap.Dispose();
            //g.Dispose();

            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    if (_cancell)
                        break;
                    try
                    {
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

        public bool WorkCrayscale()
        {
            Bitmap bmap = (Bitmap)_image;
            ImageAttributes ia = new ImageAttributes();
            ColorMatrix cm = null;
            Graphics g = null;

            g = Graphics.FromImage(bmap);
            cm = new ColorMatrix(new float[][]
                    {
                         new float[] { 0, 0, 0, 0, 0},
                         new float[] { 0, 0, 0, 0, 0},
                         new float[] { 0, 0, 0, 0, 0},
                         new float[] { 0, 0, 0, 0.25f, 0},
                         new float[] { 0, 0, 0, 0, 0}

                    });
            ia.SetColorMatrix(cm);

            g.DrawImage(bmap, new Rectangle(0, 0, bmap.Width, bmap.Height),
            0, 0, bmap.Width, bmap.Height, GraphicsUnit.Pixel, ia);
            OnProgressChanged((Bitmap)bmap.Clone());
            Thread.Sleep(500);         

            bmap.Dispose();
            g.Dispose();
            if (_image != null)
                _image.Dispose();
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
