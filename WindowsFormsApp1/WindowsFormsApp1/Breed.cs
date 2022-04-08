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

namespace WindowsFormsApp1
{
    public partial class Breed : Form
    {
        public Breed()
        {
            InitializeComponent();
            FillDogId();            
            showListBreed();
        }
        

        #region khai bao bien

        int key = 0;
        SqlConnection conn = new SqlConnection(
            @"Data Source=DESKTOP-SCBOTSE\SQLEXPRESS;Initial Catalog=QLGC;Integrated Security=True");

        #endregion

        #region su kien click chuyen form
        private void label8_Click_1(object sender, EventArgs e)
        {
            Dogs dogs = new Dogs();

            dogs.Show();
            this.Hide();
        }


        private void label10_Click(object sender, EventArgs e)
        {
            Breed breeding = new Breed();
            breeding.Show();
            this.Hide();
        }
        private void label9_Click_1(object sender, EventArgs e)
        {
            Health health = new Health();
            health.Show();
            this.Hide();

        }
        private void label11_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();
            this.Hide();
        }
        private void label12_Click(object sender, EventArgs e)
        {
            Dashboard dsh = new Dashboard();
            dsh.Show();
            this.Hide();
        }
        #endregion

        #region su kien click btn
        private void button1_Click(object sender, EventArgs e)
        {

            if (DogNameTxt.Text == ""|| DogIdbox.SelectedIndex == -1|| DogAgeTxt.Text == ""||RemarksTxt.Text=="")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    DateTime date = Convert.ToDateTime(BreedDateTxt.Value);
                    DateTime date2 = Convert.ToDateTime(PregDateTxt.Value);
                    DateTime date3 = Convert.ToDateTime(DateBornedTxt.Value);
                    string Query = "insert into Breeding  values('"+ date.ToString("MM-dd-yyyy") + "', "+DogIdbox.SelectedValue.ToString()+",'"+DogNameTxt.Text+"' , '"+ date2.ToString("MM-dd-yyyy")+"','"+ date3.ToString("MM-dd-yyyy") + "' , '"+DogAgeTxt.Text+"','"+RemarksTxt.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanhcong");
                    conn.Close();

                    showListBreed();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Chon mot chu cho de xoa");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Query = "delete from Breeding where BreedId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListBreed();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearTxt();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DogNameTxt.Text == ""|| DogIdbox.SelectedIndex == -1|| DogAgeTxt.Text == ""||RemarksTxt.Text=="")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    DateTime date = Convert.ToDateTime(BreedDateTxt.Value);
                    DateTime date2 = Convert.ToDateTime(PregDateTxt.Value);
                    DateTime date3 = Convert.ToDateTime(DateBornedTxt.Value);
                    string Query = "update Breeding  set BreedDate= '" + date.ToString("MM-dd-yyyy") + "' ,DogId="+DogIdbox.SelectedValue.ToString()+",DogName= '"+DogNameTxt.Text+"',PregDate= '"+date2.ToString("MM-dd-yyyy")+"',DateBorned= '"+date3.ToString("MM-dd-yyyy")+"',DogAge= '"+DogAgeTxt.Text+"',Remarks= '"+RemarksTxt.Text+"'  where BreedId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListBreed();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        #endregion

        #region cac phuong thuc
        private void clearTxt()
        {
            DogNameTxt.Text = "";
            DogAgeTxt.Text = "";
            RemarksTxt.Text ="";
            key = 0;

        }


        private void FillDogId()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select DogId from Dog", conn);
            SqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("DogId", typeof(int));
            dt.Load(dr);
            DogIdbox.DataSource = dt;
            DogIdbox.ValueMember = "DogId";
            conn.Close();
        }
        private void showListBreed()
        {
            conn.Open();
            string Query = "select * from Breeding";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            BreedGridView.DataSource = ds.Tables[0];

            conn.Close();

        }
        private void GetDogInfo()
        {
            conn.Open();
            string Query = "select * from Dog  where DogId="+DogIdbox.SelectedValue.ToString()+";";
            SqlCommand cmd = new SqlCommand(Query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DogNameTxt.Text=  dr["DogName"].ToString();
                DogAgeTxt.Text=  dr["Age"].ToString();
            }
            conn.Close();
        }
        #endregion

        #region cac su kien properties
        private void BreedGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            BreedDateTxt.Text = BreedGridView.SelectedRows[0].Cells[1].Value.ToString();
            DogIdbox.SelectedValue = BreedGridView.SelectedRows[0].Cells[2].Value.ToString();
            DogNameTxt.Text = BreedGridView.SelectedRows[0].Cells[3].Value.ToString();
            PregDateTxt.Text   = BreedGridView.SelectedRows[0].Cells[4].Value.ToString();
            DateBornedTxt.Text    = BreedGridView.SelectedRows[0].Cells[5].Value.ToString();
            DogAgeTxt.Text    = BreedGridView.SelectedRows[0].Cells[6].Value.ToString();
            RemarksTxt.Text     = BreedGridView.SelectedRows[0].Cells[7].Value.ToString();

            if (DogNameTxt.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(BreedGridView.SelectedRows[0].Cells[0].Value.ToString());

            }

           
        }
        private void DogIdbox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDogInfo();
        }



        #endregion

   

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

     
    }
}
