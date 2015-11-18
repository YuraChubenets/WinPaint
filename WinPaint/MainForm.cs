using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        Bitmap Image { get; set; }
        string ImagePath { get; set; }
        Item CurrentItem { get; set; }

       

        event EventHandler ImageOpenClick;
        event EventHandler ImageSaveClick;
        event EventHandler ImageChanged;
        event EventHandler ImageProcessInvert;
    }

    public partial class MainForm : Form, IWinPaint
    {
        

        Color currentColor = Color.Black;
        bool isdraw = false;
        bool choose = false;           
        int x, y, lx, ly = 0;
        float lw = 1.0f;

        List<BmpMatrixPoint> massPoints = new List<BmpMatrixPoint> { };
       

        GraphicsPath path = new GraphicsPath();
        Region region;

        public MainForm()
        {
            InitializeComponent();
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
                        Thread.Sleep(1000);
                    }
                    catch (ObjectDisposedException err)
                    {
                        Application.ExitThread();
                        MessageBox.Show(err.Message);
                    }
                }
            };
            Task task = new Task(action);
            task.Start();
        }


        #region IWinPaint    
        private Bitmap _image;            
        public Bitmap Image
        {
            get
            {
                return new Bitmap(_image);
            }

            set
            {
                this.InvokeEx(() => {
                    pictureBox1.Image = value;
                });
            }
        }

        public string ImagePath { get; set; }
        public Item CurrentItem { get; set; }
      

        public event EventHandler ImageOpenClick;
        public event EventHandler ImageSaveClick;
        public event EventHandler ImageChanged;
        public event EventHandler ImageProcessInvert;
        #endregion




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


        //for 2D shape 
        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, e);
            //-------------       

            switch (CurrentItem)
            {
                case Item.Pencil:
                    {
                     

                    } break;
                case Item.Line:
                    {
                        e.Graphics.DrawLine(new Pen(currentColor), new Point(x, y), new Point(lx, ly));
                        foreach (var p in massPoints)
                        {              
                            e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X , p.p2.Y));
                        }
                    }
                    break;
                case Item.Rectangle:
                    {
                        var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));                        
                        e.Graphics.DrawRectangle(new Pen(currentColor),rect);
                       
                        foreach (var p in massPoints)
                        {
                            e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X, p.p2.Y));
                        }

                    }
                    break;
                case Item.Round:
                    {
                        var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));
                        e.Graphics.DrawEllipse(new Pen(currentColor), rect);
                        e.Graphics.FillEllipse(new SolidBrush(Color.Red), rect);

                        foreach (var p in massPoints)
                        {
                            e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X, p.p2.Y));
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
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isdraw = true;
            //current coord
            x = e.X;
            y = e.Y;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isdraw = false;
            switch (CurrentItem)
            {
                case(Item.Pencil):
                    {
                     //-------------
                    } break;

                case (Item.Line):
                    {
                        massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly), lw, currentColor));
                    }
                    break;
                case (Item.Rectangle):
                    {
                        massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(x, ly), lw, currentColor));
                        massPoints.Add(new BmpMatrixPoint(new Point(lx, y), new Point(lx, ly), lw, currentColor));
                        massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, y), lw, currentColor));
                        massPoints.Add(new BmpMatrixPoint(new Point(lx, ly), new Point(x, ly), lw, currentColor));

                    }
                    break;
                case (Item.Round):
                    {
                        //massPoints.Add(new BmpMatrixPoint()) maybe
                     //   massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly), lw, currentColor));
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
                            lx = e.X;
                            ly = e.Y;
                            Graphics g = pictureBox1.CreateGraphics();
                            g.DrawLine(new Pen(currentColor), new Point(x, y), new Point(lx, ly));                           
                            massPoints.Add(new BmpMatrixPoint(new Point (x, y), new Point(lx, ly), lw, currentColor));

                            x = e.X;
                            y = e.Y;

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
                            Graphics g = pictureBox1.CreateGraphics();
                            var tempColor = currentColor;
                            currentColor = Color.White;
                            g.DrawLine(new Pen(currentColor), new Point(x, y), new Point(lx, ly));
                            massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly),lw, currentColor));
                            x = e.X;
                            y = e.Y;
                            currentColor = tempColor;
                        } break;


                    case Item.Fill:
                        {


                        }break;
                }
            }
            lblX.Text = e.X.ToString();
            lblY.Text = e.Y.ToString();
        }

        //---conver rectangl
        Func<Rectangle, Point[]> GetPoints = (Rectangle rectangle) =>
          {
              return new Point[4]
              {
                       new Point(rectangle.Left, rectangle.Top),
                        new Point(rectangle.Right, rectangle.Top),
                        new Point(rectangle.Left, rectangle.Bottom),
                         new Point(rectangle.Top, rectangle.Bottom)
              };
          };
        //----

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

        private async void btnInvert_Click(object sender, EventArgs e)
        {
            this.Enable(false);

            //if (ImageProcessInvert != null)
            //    ImageProcessInvert(this, EventArgs.Empty);


            Func<Image> func = (() =>
            {
                Bitmap bmap = (Bitmap)pictureBox1.Image.Clone();
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        bmap.SetPixel(i, j,
                          Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                    }

                    Thread.Sleep(1);
                    pictureBox1.Image = (Bitmap)bmap.Clone();
                }
                // pictureBox1.Image = (Bitmap)bmap.Clone();
                return (Bitmap)bmap.Clone();
            });

            Image img = await Task<Image>.Factory.StartNew(func);

            this.Enable(true);
        }



        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            massPoints.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);

        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = " bitmap |*.bmp";
            if ( openFileDialog1.ShowDialog() == DialogResult.OK)            {                         
                ImagePath = openFileDialog1.FileName;
                if (ImageOpenClick != null)
                   ImageOpenClick(this, EventArgs.Empty);
                
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "bitmap|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ImagePath = saveFileDialog1.FileName;       
               _image = new Bitmap(pictureBox1.Width, pictureBox1.Height);//to create bmp of same size as panel
                Rectangle rect = new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height); //to set bounds to image
                pictureBox1.DrawToBitmap(_image, rect);  // drawing pictureBox1 imgae into bmp of bounds of rect

                if (pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();

                if (ImageSaveClick != null)
                     ImageSaveClick(this, e);              
            }
        }      
    }
}
