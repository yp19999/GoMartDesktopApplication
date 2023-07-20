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
    public partial class Category : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");

        public Category()
        {
            InitializeComponent();
        }
        private void Category_Load(object sender, EventArgs e)
        {
            btnDeleteCat.Visible = false;
            btnUpdateCat.Visible = false;
            lblCatID.Visible    = false;
            CatgoryBind();
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            if (txtCatName.Text == string.Empty)
            {
                txtCatName.Focus();
                MessageBox.Show("Please Select UserName.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (txtrichCatDescription.Text == string.Empty)
            {
                txtrichCatDescription.Focus();
                MessageBox.Show("Please Select Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmdCheclExistenceofCat = new SqlCommand("select catName from tblCategory where catName=@catname", con);
                cmdCheclExistenceofCat.Parameters.AddWithValue("@catname", txtCatName.Text);
                con.Open();
                var result = cmdCheclExistenceofCat.ExecuteScalar();

                if (result !=null)
                {
                    MessageBox.Show("Category {0} Already Exist","Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("addCategory", con);
                    cmd.Parameters.AddWithValue("@catName", txtCatName.Text);
                    cmd.Parameters.AddWithValue("@catDescription", txtrichCatDescription.Text);
                    cmd.CommandType = CommandType.StoredProcedure;
                    int i =cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MessageBox.Show("Category Inserted Successflly.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    CatgoryBind();
                }
                con.Close();
            }
        }

        private void CatgoryBind()
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("select catID,catName,catDescription from tblCategory ", con);
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            btnUpdateCat.Visible = true;
            btnAddCat.Visible = false;
            btnDeleteCat.Visible = true;

            lblCatID.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtCatName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtrichCatDescription.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void btnUpdateCat_Click(object sender, EventArgs e)
        {
            if (lblCatID.Text == string.Empty)
            {
                MessageBox.Show("Please Select categoryID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCatID.Focus();
            }
            if (txtrichCatDescription.Text == string.Empty)
            {
                txtrichCatDescription.Focus();
                MessageBox.Show("Please Select Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                SqlCommand cmdupdateCat = new SqlCommand("updateCategory", con);
                cmdupdateCat.Parameters.AddWithValue("@catid", Convert.ToInt32(lblCatID.Text));
                cmdupdateCat.Parameters.AddWithValue("@catname", txtCatName.Text);
                cmdupdateCat.Parameters.AddWithValue("@catdescription", txtrichCatDescription.Text);
                con.Open();
                cmdupdateCat.CommandType = CommandType.StoredProcedure;
                int i = cmdupdateCat.ExecuteNonQuery();

                if (i>0)
                {
                    MessageBox.Show("Category {0} Update Successfully", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CatgoryBind();
                }
                else
                {
                    MessageBox.Show("error Not Updated","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
        }

        private void btnDeleteCat_Click(object sender, EventArgs e)
        {
            try
            {
                if(lblCatID.Text == string.Empty)
                {
                    MessageBox.Show("Please Select CatID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (lblCatID.Text != string.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Are You want to Delete Category!", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmdDeleteCat = new SqlCommand("catDelete", con);
                        cmdDeleteCat.Parameters.AddWithValue("@catid", Convert.ToInt32(lblCatID.Text));
                        con.Close();
                        con.Open();
                        cmdDeleteCat.CommandType = CommandType.StoredProcedure;
                        int i = cmdDeleteCat.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Category Delete Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CatgoryBind();
                        }
                        else
                        {
                            MessageBox.Show("Delete Failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
