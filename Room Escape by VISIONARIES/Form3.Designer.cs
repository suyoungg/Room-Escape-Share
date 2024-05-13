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
            this.lblDialogBox = new System.Windows.Forms.Label();
            this.picBeds = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBack1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBeds)).BeginInit();
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
            this.picBack2.Location = new System.Drawing.Point(468, 12);
            this.picBack2.Margin = new System.Windows.Forms.Padding(2);
            this.picBack2.Name = "picBack2";
            this.picBack2.Size = new System.Drawing.Size(114, 106);
            this.picBack2.TabIndex = 1;
            this.picBack2.TabStop = false;
            // 
            // picSprite
            // 
            this.picSprite.Image = ((System.Drawing.Image)(resources.GetObject("picSprite.Image")));
            this.picSprite.Location = new System.Drawing.Point(489, 132);
            this.picSprite.Name = "picSprite";
            this.picSprite.Size = new System.Drawing.Size(299, 317);
            this.picSprite.TabIndex = 2;
            this.picSprite.TabStop = false;
            // 
            // lblDialogBox
            // 
            this.lblDialogBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDialogBox.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDialogBox.Location = new System.Drawing.Point(167, 303);
            this.lblDialogBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDialogBox.Name = "lblDialogBox";
            this.lblDialogBox.Size = new System.Drawing.Size(452, 105);
            this.lblDialogBox.TabIndex = 3;
            this.lblDialogBox.Text = "label1";
            this.lblDialogBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picBeds
            // 
            this.picBeds.Image = ((System.Drawing.Image)(resources.GetObject("picBeds.Image")));
            this.picBeds.Location = new System.Drawing.Point(87, 52);
            this.picBeds.Name = "picBeds";
            this.picBeds.Size = new System.Drawing.Size(299, 274);
            this.picBeds.TabIndex = 4;
            this.picBeds.TabStop = false;
            // 
            // frmStorage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.picBeds);
            this.Controls.Add(this.lblDialogBox);
            this.Controls.Add(this.picSprite);
            this.Controls.Add(this.picBack2);
            this.Controls.Add(this.picBack1);
            this.Name = "frmStorage";
            this.Text = "Graphics2";
            this.Load += new System.EventHandler(this.frmStorage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBack1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBack2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSprite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBeds)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picBack1;
        public System.Windows.Forms.PictureBox picBack2;
        public System.Windows.Forms.PictureBox picSprite;
        public System.Windows.Forms.Label lblDialogBox;
        public System.Windows.Forms.PictureBox picBeds;
    }
}