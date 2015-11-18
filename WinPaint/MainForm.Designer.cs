namespace WinPaint
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCreateNew = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblCoorX = new System.Windows.Forms.Label();
            this.lblCoorY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblY = new System.Windows.Forms.Label();
            this.btnChCol = new System.Windows.Forms.Button();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnPencil = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnRectangl = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnRound = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripBtnErase = new System.Windows.Forms.ToolStripButton();
            this.btnInvert = new System.Windows.Forms.Button();
            this.btnGrayscale = new System.Windows.Forms.Button();
            this.lblEffect = new System.Windows.Forms.Label();
            this.lblOclock = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFill = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "currentColor";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(157, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(494, 305);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(109, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 2;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(200, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // btnCreateNew
            // 
            this.btnCreateNew.Location = new System.Drawing.Point(3, 12);
            this.btnCreateNew.Name = "btnCreateNew";
            this.btnCreateNew.Size = new System.Drawing.Size(61, 23);
            this.btnCreateNew.TabIndex = 4;
            this.btnCreateNew.Text = "New";
            this.btnCreateNew.UseVisualStyleBackColor = true;
            this.btnCreateNew.Click += new System.EventHandler(this.btnCreateNew_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Black;
            this.pictureBox3.Location = new System.Drawing.Point(92, 45);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(59, 37);
            this.pictureBox3.TabIndex = 6;
            this.pictureBox3.TabStop = false;
            // 
            // lblCoorX
            // 
            this.lblCoorX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCoorX.AutoSize = true;
            this.lblCoorX.Location = new System.Drawing.Point(3, 310);
            this.lblCoorX.Name = "lblCoorX";
            this.lblCoorX.Size = new System.Drawing.Size(73, 13);
            this.lblCoorX.TabIndex = 8;
            this.lblCoorX.Text = "Coordinates X";
            // 
            // lblCoorY
            // 
            this.lblCoorY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCoorY.AutoSize = true;
            this.lblCoorY.Location = new System.Drawing.Point(3, 336);
            this.lblCoorY.Name = "lblCoorY";
            this.lblCoorY.Size = new System.Drawing.Size(73, 13);
            this.lblCoorY.TabIndex = 9;
            this.lblCoorY.Text = "Coordinates Y";
            // 
            // lblX
            // 
            this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblX.AutoSize = true;
            this.lblX.Location = new System.Drawing.Point(82, 310);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(13, 13);
            this.lblX.TabIndex = 10;
            this.lblX.Text = "0";
            // 
            // lblY
            // 
            this.lblY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblY.AutoSize = true;
            this.lblY.Location = new System.Drawing.Point(82, 336);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(13, 13);
            this.lblY.TabIndex = 11;
            this.lblY.Text = "0";
            // 
            // btnChCol
            // 
            this.btnChCol.Location = new System.Drawing.Point(3, 88);
            this.btnChCol.Name = "btnChCol";
            this.btnChCol.Size = new System.Drawing.Size(148, 53);
            this.btnChCol.TabIndex = 12;
            this.btnChCol.Text = "Choose color";
            this.btnChCol.UseVisualStyleBackColor = true;
            this.btnChCol.Click += new System.EventHandler(this.btnChCol_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripBtnPencil,
            this.toolStripSeparator1,
            this.toolStripBtnLine,
            this.toolStripSeparator2,
            this.toolStripBtnRectangl,
            this.toolStripSeparator3,
            this.toolStripBtnRound,
            this.toolStripSeparator4,
            this.toolStripBtnErase});
            this.toolStrip1.Location = new System.Drawing.Point(3, 178);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(151, 25);
            this.toolStrip1.TabIndex = 13;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripBtnPencil
            // 
            this.toolStripBtnPencil.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnPencil.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnPencil.Image")));
            this.toolStripBtnPencil.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnPencil.Name = "toolStripBtnPencil";
            this.toolStripBtnPencil.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnPencil.Text = "pencil";
            this.toolStripBtnPencil.Click += new System.EventHandler(this.toolStripBtnPencil_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnLine
            // 
            this.toolStripBtnLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnLine.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnLine.Image")));
            this.toolStripBtnLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnLine.Name = "toolStripBtnLine";
            this.toolStripBtnLine.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnLine.Text = "line";
            this.toolStripBtnLine.Click += new System.EventHandler(this.toolStripBtnLine_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnRectangl
            // 
            this.toolStripBtnRectangl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnRectangl.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnRectangl.Image")));
            this.toolStripBtnRectangl.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnRectangl.Name = "toolStripBtnRectangl";
            this.toolStripBtnRectangl.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnRectangl.Text = "rectangl";
            this.toolStripBtnRectangl.Click += new System.EventHandler(this.toolStripBtnRectangl_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnRound
            // 
            this.toolStripBtnRound.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnRound.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnRound.Image")));
            this.toolStripBtnRound.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnRound.Name = "toolStripBtnRound";
            this.toolStripBtnRound.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnRound.Text = "round";
            this.toolStripBtnRound.Click += new System.EventHandler(this.toolStripBtnRound_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripBtnErase
            // 
            this.toolStripBtnErase.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripBtnErase.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnErase.Image")));
            this.toolStripBtnErase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnErase.Name = "toolStripBtnErase";
            this.toolStripBtnErase.Size = new System.Drawing.Size(23, 22);
            this.toolStripBtnErase.Text = "erase";
            this.toolStripBtnErase.Click += new System.EventHandler(this.toolStripBtnErase_Click);
            // 
            // btnInvert
            // 
            this.btnInvert.Location = new System.Drawing.Point(395, 9);
            this.btnInvert.Name = "btnInvert";
            this.btnInvert.Size = new System.Drawing.Size(65, 25);
            this.btnInvert.TabIndex = 14;
            this.btnInvert.Text = "Invert";
            this.btnInvert.UseVisualStyleBackColor = true;
            this.btnInvert.Click += new System.EventHandler(this.btnInvert_Click);
            // 
            // btnGrayscale
            // 
            this.btnGrayscale.Location = new System.Drawing.Point(466, 9);
            this.btnGrayscale.Name = "btnGrayscale";
            this.btnGrayscale.Size = new System.Drawing.Size(65, 25);
            this.btnGrayscale.TabIndex = 15;
            this.btnGrayscale.Text = "Grayscale";
            this.btnGrayscale.UseVisualStyleBackColor = true;
            // 
            // lblEffect
            // 
            this.lblEffect.AutoSize = true;
            this.lblEffect.Location = new System.Drawing.Point(354, 9);
            this.lblEffect.Name = "lblEffect";
            this.lblEffect.Size = new System.Drawing.Size(35, 13);
            this.lblEffect.TabIndex = 16;
            this.lblEffect.Text = "Effect";
            // 
            // lblOclock
            // 
            this.lblOclock.AutoSize = true;
            this.lblOclock.Location = new System.Drawing.Point(606, 9);
            this.lblOclock.Name = "lblOclock";
            this.lblOclock.Size = new System.Drawing.Size(49, 13);
            this.lblOclock.TabIndex = 17;
            this.lblOclock.Text = "00:00:00";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(270, 160);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(261, 23);
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(3, 221);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(148, 23);
            this.btnFill.TabIndex = 19;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.btnFill_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 362);
            this.Controls.Add(this.btnFill);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lblOclock);
            this.Controls.Add(this.lblEffect);
            this.Controls.Add(this.btnGrayscale);
            this.Controls.Add(this.btnInvert);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.btnChCol);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblCoorY);
            this.Controls.Add(this.lblCoorX);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.btnCreateNew);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "MainForm";
            this.Text = "WinPaint";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCreateNew;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblCoorX;
        private System.Windows.Forms.Label lblCoorY;
        private System.Windows.Forms.Label lblX;
        private System.Windows.Forms.Label lblY;
        private System.Windows.Forms.Button btnChCol;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripBtnPencil;
        private System.Windows.Forms.ToolStripButton toolStripBtnLine;
        private System.Windows.Forms.ToolStripButton toolStripBtnRectangl;
        private System.Windows.Forms.ToolStripButton toolStripBtnRound;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton toolStripBtnErase;
        private System.Windows.Forms.Button btnInvert;
        private System.Windows.Forms.Button btnGrayscale;
        private System.Windows.Forms.Label lblEffect;
        private System.Windows.Forms.Label lblOclock;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnFill;
    }
}

