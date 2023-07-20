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
    public partial class Seller : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=GoMart;Integrated Security=True");

        public Seller()
        {
            InitializeComponent();
        }

        private void btnAddCat_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmdAddSeller = new SqlCommand("addSeller", con);
                cmdAddSeller.Parameters.AddWithValue("@selname", txtSelName.Text);
                cmdAddSeller.Parameters.AddWithValue("@selage", Convert.ToInt32(txtSelAge.Text));
                cmdAddSeller.Parameters.AddWithValue("@selphone", txtSelPhone.Text);
                cmdAddSeller.Parameters.AddWithValue("@selpassword", txtPasswprd.Text);

                cmdAddSeller.CommandType= CommandType.StoredProcedure;
                
                int i = cmdAddSeller.ExecuteNonQuery();
                if (i > 0) 
                {
                    MessageBox.Show("Seller inserted successfully.", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                con.Close();
            }
            catch(Exception Ex) 
            {
                MessageBox.Show(Ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnUpdateCat_Click(object sender, EventArgs e)
        {
            SqlCommand sqlCommand = new SqlCommand("updateSeller", con);
            sqlCommand.Parameters.AddWithValue("@selid", Convert.ToInt32(lblSelD.Text));
            sqlCommand.Parameters.AddWithValue("@selname", txtSelName.Text);
            sqlCommand.Parameters.AddWithValue("@selage", Convert.ToInt32(txtSelAge.Text));
            sqlCommand.Parameters.AddWithValue("@selphone", txtSelPhone.Text);
            sqlCommand.Parameters.AddWithValue("@selpassword", txtPasswprd.Text);
            con.Open();

            sqlCommand.CommandType=CommandType.StoredProcedure;
            int i = sqlCommand.ExecuteNonQuery();
            if (i > 0)
            {
                MessageBox.Show("Successfully Updated .","success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Updation Fail.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            con.Close() ;
            bindselerData();
        }

        public void bindselerData()
        {
            con.Close();
            SqlCommand cmd = new SqlCommand("select * from tblSeller ", con);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);
            dataGridView1.DataSource = dataTable;

            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Seller_Load(object sender, EventArgs e)
        {
            bindselerData();
        }

        private void btnDeleteCat_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblSelD.Text == string.Empty)
                {
                    MessageBox.Show("Please Select CatID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (lblSelD.Text != string.Empty)
                {
                    if (DialogResult.Yes == MessageBox.Show("Are You want to Delete Category!", "Conformation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                    {
                        SqlCommand cmdDeleteCat = new SqlCommand("selDelete", con);
                        cmdDeleteCat.Parameters.AddWithValue("@selid", Convert.ToInt32(lblSelD.Text));
                        con.Close();
                        con.Open();
                        cmdDeleteCat.CommandType = CommandType.StoredProcedure;
                        int i = cmdDeleteCat.ExecuteNonQuery();
                        if (i > 0)
                        {
                            MessageBox.Show("Seller Delete Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bindselerData();
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            lblSelD.Text= dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txtSelName.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtSelAge.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txtSelPhone.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtPasswprd.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }
    }

}
