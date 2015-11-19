using System;
using System.Collections.Generic;
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
        Color currentColor = Color.Black;
        bool isdraw = false;  
        int x, y, lx, ly = 0;
        float lw = 4.0f;

        List<BmpMatrixPoint> massPoints = new List<BmpMatrixPoint> { };
       

        GraphicsPath path = new GraphicsPath();
        Region region;
        Inverter _invert;

        public MainForm()
        {
            InitializeComponent();
            btnOpen.Click += btnOpen_Click;
            btnSave.Click += BtnSave_Click;
            pictureBox1.Paint += PictureBox1_Paint;
            this.Load += MainForm_Load;
            _contentImage = new Bitmap(pictureBox1.Width, pictureBox1.Height);          
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
                        Application.ExitThread();
                        MessageBox.Show(err.Message);
                    }
                }
            };
            Task task = new Task(action);
            task.Start();
        }

        #region IWinPaint    
        //private Bitmap _image;            
        public Image SetImage(Image img)
        {
              this.InvokeEx(() =>
                 {
                     pictureBox1.Image = img;
                 });
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

        private async void btnInvert_Click(object sender, EventArgs e)
        {

            this.Enable(false);
            this.Enabled = true;
            
            _invert = new Inverter(ContentImage);
            _invert.ProcessChanged += SetImage;


            bool cancelled = await Task<bool>.Factory.StartNew(_invert.Work);

            string message = cancelled ? "Процесс отменен" : "Процесс завершен!";
            MessageBox.Show(message);
            this.Enable(true);
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
                        //SolidBrush sb = new SolidBrush(currentColor);
                        //e.Graphics.FillEllipse(sb, lx, ly, lw, lw);
                        foreach (var p in massPoints)
                        {
                            e.Graphics.DrawLines(new Pen(p.color, lw), p.p1);
                        }

                    }
                    break;
                case Item.Line:
                    {
                        //e.Graphics.FillRectangle(new Pen(currentColor), new Point(x, y), new Point(lx, ly));
                        //foreach (var p in massPoints)
                        //{              
                        //    e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X , p.p2.Y));
                        //}
                    }
                    break;
                case Item.Rectangle:
                    {
                        //var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));                        
                        //e.Graphics.DrawRectangle(new Pen(currentColor),rect);
                       
                        //foreach (var p in massPoints)
                        //{
                        //    e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X, p.p2.Y));
                        //}

                    }
                    break;
                case Item.Round:
                    {
                       // var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));
                       // e.Graphics.DrawEllipse(new Pen(currentColor), rect);
                       //// e.Graphics.FillEllipse(new SolidBrush(Color.Red), rect);

                       // foreach (var p in massPoints)
                       // {
                       //     e.Graphics.DrawLine(new Pen(p.color, lw), new Point(p.p1.X, p.p1.Y), new Point(p.p2.X, p.p2.Y));
                       // }

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
             Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
            switch (CurrentItem)
            {
                
                case (Item.Pencil):
                    {

                        SolidBrush sb = new SolidBrush(currentColor);
                     //   g.FillEllipse(sb, e.X, e.Y, lw, lw);
                        massPoints.Add(new BmpMatrixPoint(new Point[] { new Point(x, y) }, lw, currentColor));
                        //-------------


                    }
                    break;

                case (Item.Line):
                    {
                        // massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly), lw, currentColor));
                    }
                    break;
                case (Item.Rectangle):
                    {
                        
                    }
                    break;
                case (Item.Round):
                    {
                        
                    }
                    break;
            }
            g.Dispose();
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
                       // massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly), lw, currentColor));
                    }
                    break;
                case (Item.Rectangle):
                    {
                        //massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(x, ly), lw, currentColor));
                        //massPoints.Add(new BmpMatrixPoint(new Point(lx, y), new Point(lx, ly), lw, currentColor));
                        //massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, y), lw, currentColor));
                        //massPoints.Add(new BmpMatrixPoint(new Point(lx, ly), new Point(x, ly), lw, currentColor));
                    }
                    break;
                case (Item.Round):
                    {
                      //var rect = GetRectangleFromPoints(new Point(x, y), new Point(lx, ly));
                      //pictureBox1.CreateGraphics().DrawEllipse(new Pen(currentColor), rect);

                      //  GraphicsPath gp = new GraphicsPath();
                      //  Region region = new Region(rect);
                      // region.

                        //massPoints.Add(new BmpMatrixPoint()) maybe
                        //massPoints.Add(new BmpMatrixPoint(new Point(x, y), new Point(lx, ly), lw, currentColor));
                    }
                    break;
            }
            
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isdraw)
            {
                Graphics g = Graphics.FromHwnd(pictureBox1.Handle);
                switch (CurrentItem)
                {

                    case Item.Pencil:
                        {
                            lx = e.X;
                            ly = e.Y;
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                            SolidBrush sb = new SolidBrush(currentColor);
                             g.FillEllipse(sb, e.X, e.Y, lw, lw);
                            // g.DrawLine(new Pen(currentColor,lw), new Point(x, y), new Point(lx, ly));                           
                            massPoints.Add(new BmpMatrixPoint(new Point[] { new Point(x, y) }, lw, currentColor));
                            massPoints.Add(new BmpMatrixPoint(new Point[] { new Point(lx, ly) }, lw, currentColor));
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
                           
                            var tempColor = currentColor;
                            currentColor = Color.White;
                            g.DrawLine(new Pen(currentColor), new Point(x, y), new Point(lx, ly));
                            massPoints.Add(new BmpMatrixPoint(new Point[] { new Point(x, y) }, lw, currentColor));                        
                            x = e.X;
                            y = e.Y;
                            currentColor = tempColor;
                        } break;


                    case Item.Fill:
                        {


                        }break;
                }
                g.Dispose();
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
              
                using (var bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height))
                {
                   
                    FileStream fstream = new FileStream(saveFileDialog1.FileName, FileMode.Create);

                    bitmap.Save(fstream ,  ImageFormat.Bmp);
                    fstream.Close();
                   
                }
  
                //if (ImageSaveClick != null)
                //     ImageSaveClick(this, e);              
            }
        }      
    }
}
