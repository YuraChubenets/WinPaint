using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WinPaint
{
   internal class BmpMatrixPoint
    {
        public PointF[] p1 { get; set; }
        public Color color { get; set; }
        public float lw { get; set; }
        public Color? fillColor { get; set; }

        public BmpMatrixPoint(PointF[] p1, float lw, Color color, Color? fillColor = null)
        {
            this.p1 = p1;    
            this.lw = lw;
            this.color = color;
            this.fillColor = fillColor;
        }

    }
}
