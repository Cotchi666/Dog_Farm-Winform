using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Dogs : Form
    {
        public Dogs()
        {
            InitializeComponent();
            showListDog();
        }
        int age = 0;
        int key = 0;
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-SCBOTSE\SQLEXPRESS;Initial Catalog=QLGC;Integrated Security=True");

        private void showListDog()
        {
            conn.Open();
            string Query = "select * from Dog ";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            DogDGView.DataSource = ds.Tables[0];
            conn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(DogNameTxt.Text == ""|| ColorTxt.Text == ""|| BreedTxt.Text == ""|| WeightTxt.Text == ""|| AgeTxt.Text == "")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Query = "insert into Dog values('"+DogNameTxt.Text+"','"+ColorTxt.Text+"','"+BreedTxt.Text+"','"+AgeTxt.Text+"','"+WeightTxt.Text+"') ";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanhcong");                
                    conn.Close();

                    showListDog();
                    clearTxt();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

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

        private void label12_Click(object sender, EventArgs e)
        {

            Breed breeding = new Breed();
            breeding.Show();
            this.Hide();
        }

        private void DOBTxt_ValueChanged(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date-DOBTxt.Value.Date).Days)/365;
        }

        private void DOBTxt_MouseLeave(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date-DOBTxt.Value.Date).Days)/365;
            AgeTxt.Text ="" + age;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearTxt();
        }
        private void clearTxt()
        {
            
                DogNameTxt.Text = "";
                ColorTxt.Text = "";
                BreedTxt.Text = "";
                WeightTxt.Text = "";
                AgeTxt.Text = "";
            key = 0;
            
        }
        private void DogDGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            DogNameTxt.Text = DogDGView.SelectedRows[0].Cells[1].Value.ToString();
            ColorTxt.Text   = DogDGView.SelectedRows[0].Cells[2].Value.ToString();
            BreedTxt.Text   = DogDGView.SelectedRows[0].Cells[3].Value.ToString();
            AgeTxt.Text     = DogDGView.SelectedRows[0].Cells[4].Value.ToString();
            WeightTxt.Text  = DogDGView.SelectedRows[0].Cells[5].Value.ToString();
            if(DogNameTxt.Text == "")
            {
                key = 0;
                age = 0;
            }
            else
            {
                key = Convert.ToInt32(DogDGView.SelectedRows[0].Cells[0].Value.ToString());
                age = Convert.ToInt32(DogDGView.SelectedRows[0].Cells[5].Value.ToString());
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
                    string Query = "delete from Dog where DogId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListDog();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DogNameTxt.Text == ""|| ColorTxt.Text == ""|| BreedTxt.Text == ""|| WeightTxt.Text == ""|| AgeTxt.Text == "")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Query = "update Dog  set DogName='"+DogNameTxt.Text+"',Color= '"+ColorTxt.Text+"',Breed= '"+BreedTxt.Text+"',Age= '"+AgeTxt.Text+"',Weight= '"+WeightTxt.Text+"' where DogId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListDog();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
