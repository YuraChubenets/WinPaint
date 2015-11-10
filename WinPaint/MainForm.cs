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
        //path file image
        string GetImagePath { get; }
        //content
        Bitmap Image { get; set; }

        void SetImageInvert(Image image);

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
        float bw = 1.0f;
      
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
                    Invoke((Action)(() => lblOclock.Text = DateTime.Now.ToLongTimeString()));
                    Thread.Sleep(1000);
                }

            };

            Task task = new Task(action);
            task.Start();
        }



        #region IWinPaint  
        public void SetImageInvert(Image image)
        {
            this.InvokeEx(()=> {
                 pictureBox1.Image = image;
            });
           

        } 

        public string GetImagePath
        {
            get
            {
                return pictureBox1.ImageLocation;
            }
        }
        public Bitmap Image
        {
            get
            {
                return new Bitmap(pictureBox1.Width, pictureBox1.Height);
            }

            set
            {
                pictureBox1.Image = value;
            }
        }
       

        public event EventHandler ImageOpenClick;
        public event EventHandler ImageSaveClick;
        public event EventHandler ImageChanged;
        public event EventHandler ImageProcessInvert;

        #endregion


        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, EventArgs.Empty);
            //-------------         
            e.Graphics.DrawLine( new Pen(currentColor), new Point(x, y), new Point(lx, ly));

            foreach(var p in twoPoint)
            {
                e.Graphics.DrawLine(new Pen(p.color, bw),  p.X, p.Y);               
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
            pictureBox1.Paint -= PictureBox1_Paint;            
            twoPoint.Clear();
            pictureBox1.Refresh();
            pictureBox1.Paint += PictureBox1_Paint;
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
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = " bitmap |*.bmp";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(dlg.FileName); 
                if (ImageOpenClick != null) ImageOpenClick(this, EventArgs.Empty);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = pictureBox1.RectangleToScreen(pictureBox1.ClientRectangle);
            g.CopyFromScreen(rect.Location, Point.Empty, pictureBox1.Size);
            g.Dispose();

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "bitmap|*.bmp";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (ImageSaveClick != null) ImageSaveClick(this, EventArgs.Empty);
            }
        }       

       
    }
}
