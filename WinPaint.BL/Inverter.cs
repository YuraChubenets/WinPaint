using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;


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

            using (Bitmap bmapInvert = (Bitmap)_image)
            {
                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cm = null;

                using (Graphics g1 = Graphics.FromImage(bmapInvert))
                {

                    cm = new ColorMatrix(
                               new float[][]
                               {
                           new float[] {-1, 0, 0, 0, 0},
                           new float[] {0, -1, 0, 0, 0},
                           new float[] {0, 0, -1, 0, 0},
                           new float[] {0, 0, 0, 1, 0},
                           new float[] {1, 1, 1, 0, 1}
                               });
                    ia.SetColorMatrix(cm);

                    g1.DrawImage(bmapInvert, new Rectangle(0, 0, bmapInvert.Width, bmapInvert.Height),
                    0, 0, bmapInvert.Width, bmapInvert.Height, GraphicsUnit.Pixel, ia);

                    OnProgressChanged((Bitmap)bmapInvert.Clone());
                }
            }
            return _cancell;
        }

        public bool WorkCrayscale()
        {

            using (Bitmap bmap = (Bitmap)_image)
            {
                ImageAttributes ia = new ImageAttributes();
                ColorMatrix cm = null;
                using (Graphics g = Graphics.FromImage(bmap))
                {

                    cm = new ColorMatrix(new float[][]
                        {
                        new float[] {.3f, .3f, .3f, 0, 0},
                        new float[] {.59f, .59f, .59f, 0, 0},
                        new float[] {.11f, .11f, .11f, 0, 0},
                        new float[] {0, 0, 0, 1, 0},
                        new float[] {0, 0, 0, 0, 1}

                        });
                    ia.SetColorMatrix(cm);

                    g.DrawImage(bmap, new Rectangle(0, 0, bmap.Width, bmap.Height),
                    0, 0, bmap.Width, bmap.Height, GraphicsUnit.Pixel, ia);
                    OnProgressChanged((Bitmap)bmap.Clone());

                }
            }
                return _cancell;
        }


        public void OnProgressChanged( Image im)
        {
            if (ProcessChanged != null)
                ProcessChanged(im);
        }

        public event Action<Image> ProcessChanged;
    }
}
