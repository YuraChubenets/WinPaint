using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaint.BL;

namespace WinPaint
{
    public enum Item
    {
        Pencil,     
        Line,
        Rectangle,
        Round,       
        Erase,
        Fill     
    }

    public interface IWinPaint
    {
        //content
        Image SetImage(Image img);
        string ImagePath { get; set; }
        Item CurrentItem { get; set; }
        Image ContentImage { get; set;}

        event EventHandler ImageOpenClick;
        event EventHandler ImageSaveClick;
        event EventHandler ImageChanged;
        event EventHandler ImageProcessInvert;
    }

    public partial class MainForm : Form, IWinPaint
    {
        Color currentColor = Color.Tomato;
        bool isdraw = false;  
        int x, y, lx, ly = 0;
        float lw = 2.5f;

        List<BmpMatrixPoint> massPoints = new List<BmpMatrixPoint> { };


        public MainForm()
        {
            InitializeComponent();
            pictureBox3.BackColor = currentColor;
            btnOpen.Click += btnOpen_Click;
            btnSave.Click += BtnSave_Click;
            pictureBox1.Paint += PictureBox1_Paint;
            this.Load += MainForm_Load;

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Action action = () => {
                while (true)
                {
                    try {
                        Invoke((Action)(() => lblOclock.Text = DateTime.Now.ToLongTimeString()));
                        Task.Delay(1000);
                    }
                    catch (ObjectDisposedException err)
                    {
                        MessageBox.Show(err.Message);
                        Application.ExitThread();
                       
                    }
                }
            };
            Task task = Task.Factory.StartNew(action);
        }

        #region IWinPaint              
        public Image SetImage(Image img)
        {
              this.InvokeEx(() =>  {  pictureBox1.Image = img;  });      
              return img;
        }        

        public string ImagePath { get; set; }
        public Item CurrentItem { get; set; }

        private Image _contentImage;
        public Image ContentImage
        {
            get
            {
                       return _contentImage;
            }

            set
            {
                _contentImage = value;
            }
        }

        public event EventHandler ImageOpenClick;
        public event EventHandler ImageSaveClick;
        public event EventHandler ImageChanged;
        public event EventHandler ImageProcessInvert;
        #endregion

        private void btnInvert_Click(object sender, EventArgs e)
        {
            Action action = () => {
                this.InvokeEx(()=> { 
                var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
                ContentImage = bitmap;

                if (ImageChanged != null)
                    ImageChanged(this, e);
                massPoints.Clear();
            });

            };
            Task task = Task.Factory.StartNew(action);
        }

        private void btnChCol_Click(object sender, EventArgs e)
        {
            DialogResult d = colorDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                currentColor = colorDialog1.Color;
                pictureBox3.BackColor = currentColor;
            }
        }
        

        private void toolStripBtnPencil_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Pencil;
        }
        private void toolStripBtnLine_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Line;
        }
        private void toolStripBtnRectangl_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Rectangle;
        }
        private void toolStripBtnRound_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Round;
        }
        private void toolStripBtnErase_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Erase;
        }


        private void btnFill_Click(object sender, EventArgs e)
        {
            CurrentItem = Item.Fill;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isdraw = true;
            //current coord
            x = e.X;
            y = e.Y;
           
            //point
            //Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            switch (CurrentItem)
            {
                
                case (Item.Pencil):
                    {                                             
                       // massPoints.Add(new BmpMatrixPoint(new Point[] { (new Point(x, y)), (new Point(lx, ly)) }, lw, currentColor));
                        //-------------
                    }
                    break;

              
                case (Item.Erase):
                    {
                        
                    }
                    break;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isdraw = false;

            switch (CurrentItem)
            {
                case (Item.Pencil):
                    {
                        //-------------

                    }
                    break;

                case (Item.Line):
                    {
                        massPoints.Add(
                            new BmpMatrixPoint(
                                new PointF[] { (new PointF(x, y)), (new PointF(lx, ly)) },
                                lw, currentColor));
                    }
                    break;
                case (Item.Rectangle):
                    {
                        //if (x == lx | y == ly)
                        //   return;
                        GraphicsPath shape1 = new GraphicsPath();
                        shape1.AddRectangle(GetRectangleFromPoints(new Point(x, y), new Point(lx, ly)));

                        PointF[] ps = shape1.PathPoints;
                        massPoints.Add(
                            new BmpMatrixPoint(ps, lw, currentColor));
                        shape1.Dispose();
                    }
                    break;
                case (Item.Round):
                    {
                        GraphicsPath shape1 = new GraphicsPath();
                        shape1.AddEllipse(GetRectangleFromPoints(new Point(x, y), new Point(lx, ly)));
                        PointF[] ps = shape1.PathPoints;
                        massPoints.Add(
                            new BmpMatrixPoint(ps, lw, currentColor));
                        shape1.Dispose();
                    }
                    break;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdraw)
            {               
                switch (CurrentItem)
                { 
                    case Item.Pencil:
                        {
                            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                            lx = e.X;
                            ly = e.Y;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            SolidBrush sb = new SolidBrush(currentColor);                                                
                            g.DrawLine(new Pen(currentColor,lw), new Point(x, y), new Point(lx, ly));                  

                            massPoints.Add(
                                new BmpMatrixPoint(
                                    new PointF[] {(new Point(x, y)),(new Point(lx, ly))}, lw, currentColor));

                            x = e.X;
                            y = e.Y;
                            g.Dispose();
                        }break;

                    case Item.Line:
                        {
                           
                            lx = e.X;
                            ly = e.Y;
                            pictureBox1.Invalidate();//redrawn  pictureBox1

                        }break;                 
                        
                    case Item.Rectangle:
                        {
                            lx = e.X;
                            ly = e.Y;
                            pictureBox1.Invalidate();//redrawn  pictureBox1       and call event Paint                    

                        }break;

                    case Item.Round:
                        {
                            lx = e.X;
                            ly = e.Y;
                            pictureBox1.Invalidate();//redrawn  pictureBox1     and call event Paint    

                        }break;

                    case Item.Erase:
                        {
                            lx = e.X;
                            ly = e.Y;
                            Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            SolidBrush sb = new SolidBrush(currentColor);

                            g.DrawLine(new Pen(Color.White, lw), new Point(x, y), new Point(lx, ly));

                            massPoints.Add(
                                new BmpMatrixPoint(
                                    new PointF[] { (new Point(x, y)), (new Point(lx, ly)) }, lw, currentColor));

                            x = e.X;
                            y = e.Y;
                            g.Dispose();
                        } break;


                    case Item.Fill:
                        {


                        }break;
                }          
            }            
            lblX.Text = e.X.ToString();
            lblY.Text = e.Y.ToString();
        }
        
        //for 2D shape 
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //if (ImageChanged != null)
            //    ImageChanged(this, e);
            //-------------       

            this.InvokeEx(()=>{

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                switch (CurrentItem)
                {
                    case Item.Pencil:
                        {
                            foreach (var p in massPoints)
                            {
                                using (GraphicsPath gp = new GraphicsPath())
                                {
                                    if (p.p1.Length <= 2)
                                        gp.AddLine(p.p1[0], p.p1[1]);
                                    else
                                    if (p.p1.Length <= 4)
                                        gp.AddRectangle(GetRectangleFromPoints(Point.Round(p.p1[0]), Point.Round(p.p1[2])));
                                    else
                                    {
                                        gp.AddEllipse(GetRectangleFromPoints(new Point((int)p.p1[6].X, (int)p.p1[8].Y), new Point((int)p.p1[0].X, (int)p.p1[3].Y)));
                                    }

                                    e.Graphics.DrawPath(new Pen(p.color, p.lw), gp);
                                }
                            }

                        }
                        break;
                    case Item.Line:
                        {
                            e.Graphics.DrawLine(new Pen(currentColor, lw), new Point(x, y), new Point(lx, ly));

                            foreach (var p in massPoints)
                            {
                                using (GraphicsPath gp = new GraphicsPath())
                                {
                                    if (p.p1.Length <= 2)
                                        gp.AddLine(p.p1[0], p.p1[1]);
                                    else
                                    if (p.p1.Length <= 4)
                                        gp.AddRectangle(GetRectangleFromPoints(Point.Round(p.p1[0]), Point.Round(p.p1[2])));
                                    else
                                    {
                                        gp.AddEllipse(GetRectangleFromPoints(new Point((int)p.p1[6].X, (int)p.p1[8].Y), new Point((int)p.p1[0].X, (int)p.p1[3].Y)));
                                    }

                                    e.Graphics.DrawPath(new Pen(p.color, p.lw), gp);
                                }
                            }
                        }
                        break;
                    case Item.Rectangle:
                        {
                            var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));
                            e.Graphics.DrawRectangle(new Pen(currentColor, lw), rect);

                            foreach (var p in massPoints)
                            {
                                using (GraphicsPath gp = new GraphicsPath())
                                {
                                    if (p.p1.Length <= 2)
                                        gp.AddLine(p.p1[0], p.p1[1]);
                                    else
                                    if (p.p1.Length <= 4)
                                        gp.AddRectangle(GetRectangleFromPoints(Point.Round(p.p1[0]), Point.Round(p.p1[2])));
                                    else
                                    {
                                        gp.AddEllipse(GetRectangleFromPoints(new Point((int)p.p1[6].X, (int)p.p1[8].Y), new Point((int)p.p1[0].X, (int)p.p1[3].Y)));
                                    }

                                    e.Graphics.DrawPath(new Pen(p.color, p.lw), gp);
                                }
                            }

                        }
                        break;
                    case Item.Round:
                        {
                            var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));
                            e.Graphics.DrawEllipse(new Pen(currentColor, lw), rect);

                            foreach (var p in massPoints)
                            {
                                using (GraphicsPath gp = new GraphicsPath())
                                {
                                    if (p.p1.Length <= 2)
                                        gp.AddLine(p.p1[0], p.p1[1]);
                                    else
                                    if (p.p1.Length <= 4)
                                        gp.AddRectangle(GetRectangleFromPoints(Point.Round(p.p1[0]), Point.Round(p.p1[2])));
                                    else
                                    {
                                        gp.AddEllipse(GetRectangleFromPoints(new Point((int)p.p1[6].X, (int)p.p1[8].Y), new Point((int)p.p1[0].X, (int)p.p1[3].Y)));
                                    }

                                    e.Graphics.DrawPath(new Pen(p.color, p.lw), gp);
                                }
                            }

                        }
                        break;
                    case Item.Erase:
                        {

                        }
                        break;
                    case Item.Fill:
                        {

                        }
                        break;
                }


            });
        }

        //---------------
        protected Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            Point oPoint;
            Rectangle rect;

            if ((p2.X > p1.X) && (p2.Y > p1.Y))
            {
                rect = new Rectangle(p1, new Size(p2.X - p1.X, p2.Y - p1.Y));
            }
            else if ((p2.X < p1.X) && (p2.Y < p1.Y))
            {
                rect = new Rectangle(p2, new Size(p1.X - p2.X, p1.Y - p2.Y));
            }
            else if ((p2.X > p1.X) && (p2.Y < p1.Y))
            {
                oPoint = new Point(p1.X, p2.Y);
                rect = new Rectangle(oPoint, new Size(p2.X - p1.X, p1.Y - oPoint.Y));
            }
            else
            {
                oPoint = new Point(p2.X, p1.Y);
                rect = new Rectangle(oPoint, new Size(p1.X - p2.X, p2.Y - p1.Y));
            }
            return rect;
        }
        //-----------------------

         

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            massPoints.Clear();
            pictureBox1.CreateGraphics().Clear(Color.White);

            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
               openFileDialog1.Filter = " bitmap |*.bmp";
            if ( openFileDialog1.ShowDialog() == DialogResult.OK)
            {                         
                ImagePath = openFileDialog1.FileName;
                if (ImageOpenClick != null)
                    ImageOpenClick(this, EventArgs.Empty);
                
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "bmp|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImagePath = saveFileDialog1.FileName;
                var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);   
                ContentImage = bitmap;                         
                pictureBox1.DrawToBitmap(bitmap, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
 
                               
                 if (ImageSaveClick != null)
                       ImageSaveClick(this, e);
            }
        }      
    }
}
