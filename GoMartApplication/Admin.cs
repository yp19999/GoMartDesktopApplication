using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoMartApplication
{
    public partial class Admin : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");

        public Admin()
        {
            InitializeComponent();
        }
        private void Admin_Load(object sender, EventArgs e)
        {
            AdmiBind();
        }
        private void AdmiBind()
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("select * from tblAdmin", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            con.Close();
        }
        private void btnAddAdmin_Click(object sender, EventArgs e)
        {
            if (txtAdminId.Text == string.Empty)
            {
                txtAdminId.Focus();
                MessageBox.Show("Please Insert AdminID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtPassword.Text == string.Empty)
            {
                txtPassword.Focus();
                MessageBox.Show("Please insert Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtFullname.Text == string.Empty)
            {
                txtPassword.Focus();
                MessageBox.Show("Please insert FullName.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmdCheclExistenceofCat = new SqlCommand("Select top 1 AdminID,Password from tblAdmin where AdminID = @adminID And Password=@password", con);
                cmdCheclExistenceofCat.Parameters.AddWithValue("@adminID", txtAdminId.Text);
                cmdCheclExistenceofCat.Parameters.AddWithValue("@password", txtPassword.Text);
                con.Open();
                var result = cmdCheclExistenceofCat.ExecuteScalar();

                if (result != null)
                {
                    MessageBox.Show("Category {0} Already Exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("addAdmin", con);
                    cmd.Parameters.AddWithValue("@adminid", txtAdminId.Text);
                    cmd.Parameters.AddWithValue("@adminpass", txtPassword.Text);
                    cmd.Parameters.AddWithValue("@adminfullName", txtFullname.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category Inserted Successflly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    AdmiBind();
                }
                con.Close();
            }
        }
        private void btnUpdateAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminId.Text == string.Empty)
                {
                    MessageBox.Show("Please insert AdminID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtAdminId.Focus();
                }
                if (txtPassword.Text == string.Empty)
                {
                    txtPassword.Focus();
                    MessageBox.Show("Please Insert Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                else
                { 
                    SqlCommand cmdupdateAdmin = new SqlCommand("updateAdmin", con);
                    cmdupdateAdmin.Parameters.AddWithValue("@adminid", lblAdminID.Text);
                    cmdupdateAdmin.Parameters.AddWithValue("@adminpass", txtPassword.Text);
                    cmdupdateAdmin.Parameters.AddWithValue("@adminfullname", txtFullname.Text);
                    con.Open();
                    cmdupdateAdmin.CommandType = CommandType.StoredProcedure;

                    int i = cmdupdateAdmin.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("Category {0} Update Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        AdmiBind();
                    }
                    else
                    {
                        MessageBox.Show("error Not Updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnDeleteAdmin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminId.Text == string.Empty)
                {
                    MessageBox.Show("Please Select CatID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (txtPassword.Text != string.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Are You want to Delete Category!", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmdDeleteCat = new SqlCommand("deleteadmin", con);
                        cmdDeleteCat.Parameters.AddWithValue("@adminid", txtAdminId.Text);
                        con.Close();
                        con.Open();
                        cmdDeleteCat.CommandType = CommandType.StoredProcedure;
                        int i = cmdDeleteCat.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category Delete Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AdmiBind();
                        }
                        else
                        {
                            MessageBox.Show("Delete Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_Click_1(object sender, EventArgs e)
        {
            btnUpdateAdmin.Visible = true;
            btnAddAdmin.Visible = false;
            btnDeleteAdmin.Visible = true;


            lblAdminID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtAdminId.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtPassword.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtFullname.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }
    }
}