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
        #region
        #endregion

        #region khai bao bien
        int key = 0;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-SCBOTSE\SQLEXPRESS;Initial Catalog=QLGC;Integrated Security=True");
        #endregion
        #region su kien click
        private void label8_Click(object sender, EventArgs e)
        {
            Dogs dogs = new Dogs();

            dogs.Show();
            this.Hide();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Health health = new Health();
            health.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Breed breeding = new Breed();
            breeding.Show();
            this.Hide();
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {

            if (DogNameTxt.Text == ""|| BreedDateTxt.Text == ""|| DogIdbox.SelectedIndex == -1|| PregDateTxt.Text == ""|| DateBornedTxt.Text == ""|| DogAgeTxt.Text == "")
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
                    string Query = "insert into Breeding  values('"+ date.ToString("MM-dd-yyyy") + "', '"+DogIdbox.SelectedIndex.ToString()+"','"+DogNameTxt.Text+"' , '"+ date2.ToString("MM-dd-yyyy")+"','"+ date3.ToString("MM-dd-yyyy") + "' , '"+DogAgeTxt.Text+"','"+RemarksTxt.Text+"')";
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

        private void clearTxt()
        {

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
    
        private void DogIdbox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDogInfo();
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
                    string Query = "delete from Dog where DogId =" + key + ";";
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

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
