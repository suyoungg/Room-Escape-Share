namespace Room_Escape_by_VISIONARIES
{
    partial class frmGraphic
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
            this.btnTransition = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTransition
            // 
            this.btnTransition.Location = new System.Drawing.Point(421, 144);
            this.btnTransition.Margin = new System.Windows.Forms.Padding(2);
            this.btnTransition.Name = "btnTransition";
            this.btnTransition.Size = new System.Drawing.Size(145, 56);
            this.btnTransition.TabIndex = 0;
            this.btnTransition.Text = "Change to Form 1";
            this.btnTransition.UseVisualStyleBackColor = true;
            this.btnTransition.Click += new System.EventHandler(this.btnTransition_Click);
            // 
            // frmGraphic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnTransition);
            this.Name = "frmGraphic";
            this.Text = "Graphics";
            this.Load += new System.EventHandler(this.frmGraphic_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmGraphic_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTransition;
    }
}