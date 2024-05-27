namespace Room_Escape_by_VISIONARIES
{
    partial class frmStorage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStorage));
            this.picBack1 = new System.Windows.Forms.PictureBox();
            this.picBack2 = new System.Windows.Forms.PictureBox();
            this.picSprite = new System.Windows.Forms.PictureBox();
            this.picRiddler = new System.Windows.Forms.PictureBox();
            this.picBack3 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBack1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRiddler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack3)).BeginInit();
            this.SuspendLayout();
            // 
            // picBack1
            // 
            this.picBack1.Image = ((System.Drawing.Image)(resources.GetObject("picBack1.Image")));
            this.picBack1.Location = new System.Drawing.Point(12, 12);
            this.picBack1.Name = "picBack1";
            this.picBack1.Size = new System.Drawing.Size(451, 209);
            this.picBack1.TabIndex = 0;
            this.picBack1.TabStop = false;
            this.picBack1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // picBack2
            // 
            this.picBack2.Image = ((System.Drawing.Image)(resources.GetObject("picBack2.Image")));
            this.picBack2.Location = new System.Drawing.Point(291, 12);
            this.picBack2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picBack2.Name = "picBack2";
            this.picBack2.Size = new System.Drawing.Size(298, 269);
            this.picBack2.TabIndex = 1;
            this.picBack2.TabStop = false;
            // 
            // picSprite
            // 
            this.picSprite.Image = ((System.Drawing.Image)(resources.GetObject("picSprite.Image")));
            this.picSprite.Location = new System.Drawing.Point(584, 28);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(179, 317);
            this.picSprite.TabIndex = 2;
            this.picSprite.TabStop = false;
            // 
            // picRiddler
            // 
            this.picRiddler.Image = ((System.Drawing.Image)(resources.GetObject("picRiddler.Image")));
            this.picRiddler.Location = new System.Drawing.Point(479, 12);
            this.picRiddler.Name = "picRiddler";
            this.picRiddler.Size = new System.Drawing.Size(199, 336);
            this.picRiddler.TabIndex = 5;
            this.picRiddler.TabStop = false;
            // 
            // picBack3
            // 
            this.picBack3.Image = ((System.Drawing.Image)(resources.GetObject("picBack3.Image")));
            this.picBack3.Location = new System.Drawing.Point(129, 83);
            this.picBack3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picBack3.Name = "picBack3";
            this.picBack3.Size = new System.Drawing.Size(147, 129);
            this.picBack3.TabIndex = 6;
            this.picBack3.TabStop = false;
            // 
            // frmStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picBack3);
            this.Controls.Add(this.picRiddler);
            this.Controls.Add(this.picSprite);
            this.Controls.Add(this.picBack2);
            this.Controls.Add(this.picBack1);
            this.Name = "frmStorage";
            this.Text = "Graphics2";
            this.Load += new System.EventHandler(this.frmStorage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBack1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRiddler)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picBack1;
        public System.Windows.Forms.PictureBox picBack2;
        public System.Windows.Forms.PictureBox picBack3;
        public System.Windows.Forms.PictureBox picSprite;
        public System.Windows.Forms.PictureBox picRiddler;
    }
}