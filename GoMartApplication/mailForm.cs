using System;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class mailForm : Form
    {
        public mailForm()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mailForm_Load(object sender, EventArgs e)
        {
            if (Login.LoginName !=null)
            {
                lblUserName.Text = Login.LoginName;
            }
            if (Login.LoginType != null && Login.LoginType=="Normal User" ) 
            {
                productToolStripMenuItem.Enabled = false;
                addUserToolStripMenuItem.Enabled = false;
            }
        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Category catFrm = new Category();
            catFrm.Show();
        }

        private void mailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void mailForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dilog = MessageBox.Show("Are you sure close Application!", "CLOSE", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (dilog == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                Application.Exit();
            }
        }

        private void sellerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Seller sl=new Seller();
            sl.Show();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin ad = new Admin();
            ad.Show();
        }
    }
}
