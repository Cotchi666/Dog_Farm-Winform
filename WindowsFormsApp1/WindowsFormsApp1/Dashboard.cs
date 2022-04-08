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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CountAll();
            CountEmp();
            CountBreeding();
            CountHealth();
        }
        SqlConnection conn = new SqlConnection(
           @"Data Source=DESKTOP-SCBOTSE\SQLEXPRESS;Initial Catalog=QLGC;Integrated Security=True");
        #region chuyen form
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
        private void label11_Click(object sender, EventArgs e)
        {
            Employee emp = new Employee();
            emp.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Dashboard ds = new Dashboard();
            ds.Show();
            this.Hide();
        }
        #endregion

        #region su kien count
        private void CountAll()
        {
            conn.Open();

            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from Dog", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            DogCountTxt.Text = dt.Rows[0][0].ToString();



            conn.Close();

        }
        private void CountEmp()
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from Employee", conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            EmployeeTxt.Text = dt.Rows[0][0].ToString();
            conn.Close();

        }
        private void CountHealth()
        {
            conn.Open();

            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from Health", conn);
            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            Health.Text = dt2.Rows[0][0].ToString();

            conn.Close();

        }
        private void CountBreeding()
        {
            conn.Open();

            SqlDataAdapter sda1 = new SqlDataAdapter("select Count(*) from Breeding", conn);
            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            BreedingTxt.Text = dt1.Rows[0][0].ToString();
            conn.Close();

        }

        #endregion

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
    }
}
