using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaint.BL;

namespace WinPaint
{
    public interface IWinPaint
    {
        //content
        Bitmap Image { get; set; }
        string ImagePath { get; set; }

        event EventHandler ImageOpenClick;
        event EventHandler ImageSaveClick;
        event EventHandler ImageChanged;
        event EventHandler ImageProcessInvert;
    }

    public partial class MainForm : Form, IWinPaint
    {
        public enum Item
        {
            Rectangle,
            Line,
            Brush,
            Pencil
        }

        Color currentColor = Color.Black;
        bool isdraw = false;
        bool choose = false;           
        int x, y, lx, ly = 0;
        float lw = 1.0f;
      
        List<TwoPoint> twoPoint = new List<TwoPoint> { };
        Item currentItem;


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

        public event EventHandler ImageOpenClick;
        public event EventHandler ImageSaveClick;
        public event EventHandler ImageChanged;
        public event EventHandler ImageProcessInvert;

        #endregion


        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, e);

            //-------------         
            e.Graphics.DrawLine( new Pen(currentColor), new Point(x, y), new Point(lx, ly));
            foreach(var p in twoPoint)
            {
                e.Graphics.DrawLine(new Pen(p.color, lw),  p.X, p.Y);               
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
            twoPoint.Add(new TwoPoint( new Point(x, y), new Point(lx, ly), currentColor));
        }

        private void btnChCol_Click(object sender, EventArgs e)
        {
            DialogResult d = colorDialog1.ShowDialog();
            if( d == DialogResult.OK)
            {
                currentColor = colorDialog1.Color;
                pictureBox3.BackColor = currentColor;
            }
        }

        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            twoPoint.Clear();
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);                  

        }

        private void btnInvert_Click(object sender, EventArgs e)
        {
            btnInvert.Enabled = false;

            if (ImageProcessInvert != null)
                ImageProcessInvert(this, EventArgs.Empty);

            btnInvert.Enabled = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
               if (isdraw)
                  {
                      lx = e.X;
                      ly = e.Y;
                      pictureBox1.Invalidate();//redrawn  pictureBox1
                  }
            lblX.Text = e.X.ToString();
            lblY.Text = e.Y.ToString();
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
                pictureBox1.DrawToBitmap(_image, rect);  // drawing panel1 imgae into bmp of bounds of rect
                if(pictureBox1.Image != null)
                    pictureBox1.Image.Dispose();

                if (ImageSaveClick != null)
                     ImageSaveClick(this, e);
               // bmp.Save(ImagePath, System.Drawing.Imaging.ImageFormat.Bmp); //save location and type
            }
        }      
    }
}
