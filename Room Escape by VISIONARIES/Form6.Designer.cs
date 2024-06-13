namespace Room_Escape_by_VISIONARIES
{
    partial class frmMini1
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblHelp = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Stencil", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(176, 28);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(305, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Spot the Difference";
            // 
            // lblHelp
            // 
            this.lblHelp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHelp.Font = new System.Drawing.Font("Courier New", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.ForeColor = System.Drawing.Color.Black;
            this.lblHelp.Location = new System.Drawing.Point(576, 19);
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.Size = new System.Drawing.Size(59, 50);
            this.lblHelp.TabIndex = 17;
            this.lblHelp.Text = "?";
            this.lblHelp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblHelp.Click += new System.EventHandler(this.lblHelp_Click);
            // 
            // lblCount
            // 
            this.lblCount.AutoSize = true;
            this.lblCount.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCount.Location = new System.Drawing.Point(193, 71);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(241, 21);
            this.lblCount.TabIndex = 18;
            this.lblCount.Text = "Differences found 0/4";
            // 
            // frmMini1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 626);
            this.Controls.Add(this.lblCount);
            this.Controls.Add(this.lblHelp);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMini1";
            this.Text = "Form6";
            this.Load += new System.EventHandler(this.frmMini1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMini1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHelp;
        private System.Windows.Forms.Label lblCount;
    }
}