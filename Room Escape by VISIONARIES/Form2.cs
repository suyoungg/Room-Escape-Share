using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Room_Escape_by_VISIONARIES
{
    public partial class frmGraphic : Form
    {
        public static frmGraphic instance;
        Graphics g;
        Bitmap backbuffer2;
        frmStorage frmG = new frmStorage();

        public frmGraphic()
        {
            InitializeComponent();
            instance = this;
        }

        private void frmGraphic_Load(object sender, EventArgs e)
        {
            backbuffer2 = new Bitmap(frmG.picBack2.Image, Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        }

        private void frmGraphic_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(backbuffer2, 0, 0, backbuffer2.Width, backbuffer2.Height);
        }

        private void btnTransition_Click(object sender, EventArgs e)
        {
            frmMain form = new frmMain();
            form.Show();
        }
    }
}
