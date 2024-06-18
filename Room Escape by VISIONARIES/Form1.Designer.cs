namespace Room_Escape_by_VISIONARIES
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.picArrowL = new System.Windows.Forms.PictureBox();
            this.picArrowR = new System.Windows.Forms.PictureBox();
            this.idleTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picArrowL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArrowR)).BeginInit();
            this.SuspendLayout();
            // 
            // picArrowL
            // 
            this.picArrowL.BackColor = System.Drawing.Color.Gainsboro;
            this.picArrowL.Image = ((System.Drawing.Image)(resources.GetObject("picArrowL.Image")));
            this.picArrowL.Location = new System.Drawing.Point(60, 585);
            this.picArrowL.Name = "picArrowL";
            this.picArrowL.Size = new System.Drawing.Size(46, 50);
            this.picArrowL.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picArrowL.TabIndex = 2;
            this.picArrowL.TabStop = false;
            this.picArrowL.Click += new System.EventHandler(this.picArrowL_Click);
            // 
            // picArrowR
            // 
            this.picArrowR.BackColor = System.Drawing.Color.Gainsboro;
            this.picArrowR.Image = ((System.Drawing.Image)(resources.GetObject("picArrowR.Image")));
            this.picArrowR.Location = new System.Drawing.Point(1300, 730);
            this.picArrowR.Name = "picArrowR";
            this.picArrowR.Size = new System.Drawing.Size(48, 50);
            this.picArrowR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picArrowR.TabIndex = 3;
            this.picArrowR.TabStop = false;
            this.picArrowR.Click += new System.EventHandler(this.picArrowR_Click);
            // 
            // idleTimer
            // 
            this.idleTimer.Tick += new System.EventHandler(this.idleTimer_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 573);
            this.Controls.Add(this.picArrowR);
            this.Controls.Add(this.picArrowL);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.Text = "Prison Escape";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picArrowL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArrowR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox picArrowL;
        private System.Windows.Forms.PictureBox picArrowR;
        private System.Windows.Forms.Timer idleTimer;
    }
}

