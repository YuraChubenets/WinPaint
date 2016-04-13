using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinPaint.BL;

namespace WinPaint
{
    public partial class MainForm : Form, IWinPaint<Image>
    {
        public MainForm()
        {
            InitializeComponent();
        }
        

        public Image ContentImage
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public Item CurrentItem
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public string ImagePath
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event EventHandler ImageGrayscale;
        public event EventHandler ImageInvert;
        public event EventHandler ImageNewClick;
        public event EventHandler ImageOpenClick;
        public event EventHandler ImageSaveClick;

        public void InitialSetings()
        {
            throw new NotImplementedException();
        }

        public void SetContorlEnable(bool flag)
        {
            throw new NotImplementedException();
        }

        public void SetImage(Image img)
        {
            throw new NotImplementedException();
        }

        public void ShowProgress(int progressPercentage)
        {
            throw new NotImplementedException();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Action action = () =>
            {
                while (true)
                {
                    this.InvokeEx(() => {
                        lblOclock.Text = DateTime.Now.ToLongTimeString();
                    });
                    Thread.Sleep(1000);
                }
            };
            Task.Factory.StartNew(action);
        }
    }
}
