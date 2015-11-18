using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinPaint
{
  public   class BmpMatrixPoint
    {
        public Point p1;
        public Point p2;
        public Color color;
        public float lw;

        public BmpMatrixPoint(Point p1, Point p2, float lw,  Color color)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.lw = lw;
            this.color = color;
        }
        
    }
}
