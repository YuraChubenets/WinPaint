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
        public void Cancel()
        {
            _cancell = true;
        }

        public bool Work(Image image)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    if (_cancell)
                        break;
                    Thread.Sleep(100);
                    OnProgressChanged(i, j, image);
                }
            }
            return _cancell;
        }

        public void OnProgressChanged(int i, int j, Image image)
        {
            if (ProcessChanged != null)
                ProcessChanged(i, j, image);
        }

        public event Action<int, int, Image> ProcessChanged;
    }
}
