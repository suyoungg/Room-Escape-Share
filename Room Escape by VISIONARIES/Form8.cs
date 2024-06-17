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
    public partial class frmEndScreen : Form
    {
        public frmEndScreen()
        {
            InitializeComponent();
        }

        private void frmEndScreen_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.Hide();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmStart Start = new frmStart();
            Start.ShowDialog();
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
