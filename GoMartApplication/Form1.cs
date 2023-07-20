using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class Login : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");
        public static string LoginName,LoginType;

        #region Events
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
                    if (cmbRole.SelectedIndex > 0 && txtUser.Text !=string.Empty && txtPass.Text !=string.Empty)
                    {
                        if(cmbRole.Text== "Admin") 
                        {
                            SqlCommand sqlCommand = new SqlCommand("Select top 1 AdminID,Password,FullName from tblAdmin where AdminID=@adminID And Password=@password", con);
                            sqlCommand.Parameters.AddWithValue("@adminID", txtUser.Text);
                            sqlCommand.Parameters.AddWithValue("@password", txtPass.Text);

                            con.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                            DataTable dt=new DataTable();
                            adapter.Fill(dt);
                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Login Success! Welcome","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                LoginName = txtUser.Text;
                                LoginType = cmbRole.Text;
                                Clear();
                                this.Hide();
                                mailForm frm = new mailForm();
                                frm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Login Faild ! Please Check Your UserID and Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            }
                            
                        }   
                        else if (cmbRole.Text == "Normal User")
                        {
                            SqlCommand sqlCommand1 = new SqlCommand("Select top 1 selName,selPassword from tblSeller where selName=@selname And selPassword=@selpass", con);
                            sqlCommand1.Parameters.AddWithValue("@selname", txtUser.Text);
                            sqlCommand1.Parameters.AddWithValue("@selpass", txtPass.Text);

                            con.Close();
                            con.Open();
                            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand1);
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            con.Close();
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Login Success! Welcome", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoginName = txtUser.Text;
                                LoginType = cmbRole.Text;
                                Clear();
                                this.Hide();
                                mailForm frm = new mailForm();
                                frm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Login Faild ! Please Check Your UserID and Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please Enter valid UserName and Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please Select Any Role","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        #endregion 

        #region Methods
        public void Clear()
        {
            cmbRole.SelectedIndex = 0;
            txtPass.Clear();
            txtUser.Clear();
        }
        #endregion
    }
}
