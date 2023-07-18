using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cmbRole.SelectedIndex = 0;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if(cmbRole.SelectedIndex > 0) 
                {
                    if (txtUser.Text==string.Empty)
                    {
                        txtUser.Focus();
                        MessageBox.Show("Please Select UserName.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (txtPass.Text==string.Empty)
                    {
                        txtPass.Focus();
                        MessageBox.Show("Please Select Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (cmbRole.SelectedIndex > 0 && txtUser.Text==string.Empty && txtPass.Text==string.Empty)
                    {

                    }
                    else
                    { 

                    }
                }
                else
                {
                    MessageBox.Show("Please Select Any Role","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    cmbRole.SelectedIndex = 0;
                    txtPass.Clear();
                    txtUser.Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
