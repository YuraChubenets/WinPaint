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
        public PointF[] p1;    
        public Color color;
        public float lw;
        public Color? fillColor;

        public BmpMatrixPoint(PointF[] p1,  float lw,  Color color, Color? fillColor=null)
        {
            this.p1 = p1;
            //
            this.lw = lw;
            this.color = color;
            this.fillColor = fillColor;
        }
        
    }
}
