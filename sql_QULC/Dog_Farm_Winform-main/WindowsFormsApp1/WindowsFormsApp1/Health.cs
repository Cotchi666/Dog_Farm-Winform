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
    public partial class Health : Form
    {
        public Health()
        {
            InitializeComponent();
            FillDogId();
            showListHealth();
        }
        int key = 0;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.strSQL);
        private void FillDogId()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select DogId from Dog", conn);  
            SqlDataReader dr = cmd.ExecuteReader(); 
            DataTable dt = new DataTable();
            dt.Columns.Add("DogId", typeof(int));
            dt.Load(dr);
            DogIdBox.DataSource = dt;
            DogIdBox.ValueMember = "DogId";
            conn.Close();
        }
        private void showListHealth()
        {
            conn.Open();
            string Query = "SELECT TOP (1000) [ReportId]N'id'"+
      ",[DogId]N'Mã số chó'"+
   "   ,[DogName] N'Tên chó'"+
     " ,[ReportDate] N'Ngày khai báo'"+
      ",[Event] N'Nguyên nhân'"+
     " ,[Diagnosis] N'Chuẩn đoán'"+
      ",[Cost] N'Chi phí'"+
     " ,[Vetname] N'Tên bác sĩ'"+
     " ,[Treatment] N'Biện pháp'"+
  "FROM[QLGC].[dbo].[Health]";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            HealthGView.DataSource = ds.Tables[0];
           
            conn.Close();

        }
        private void GetDogName()
        {
            conn.Open();
            string Query = "select * from Dog  where DogId="+DogIdBox.SelectedValue.ToString()+";";
            SqlCommand cmd = new SqlCommand(Query, conn);
            DataTable dt = new DataTable();
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                DogNameTxt.Text=  dr["DogName"].ToString();
            }
            conn.Close();
        }

        private void DogIdBox_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetDogName();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                if (DogIdBox.SelectedIndex == -1|| DogNameTxt.Text == "" || DiagTxt.Text == ""|| EventTxt.Text == ""|| TreatTxt.Text == ""|| CostTxt.Text == ""|| VetTxt.Text == "")
                {
                    MessageBox.Show("Chua dien day du thong tin");
                }
                else
                {
                    try
                    {
                        conn.Open();
                    
                    DateTime date = Convert.ToDateTime(ReportDate.Value);
                    string Query = "insert into Health  values(" + DogIdBox.SelectedValue.ToString() + ", N'"+DogNameTxt.Text+"','"+date.ToString("MM-dd-yyyy")+"' , N'"+EventTxt.Text+"',N'"+DiagTxt.Text+ "' , '"+CostTxt.Text+"',N'"+VetTxt.Text+"',N'"+TreatTxt.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thanh cong");
                        conn.Close();

                    showListHealth();
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

            DogNameTxt.Text ="";
            DiagTxt.Text = "";
            EventTxt.Text = "";
            TreatTxt.Text = "";
            CostTxt.Text = "";
            VetTxt.Text = "";
            key = 0;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            clearTxt();
        }

        private void HealthGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DogIdBox.SelectedValue = HealthGView.SelectedRows[0].Cells[1].Value.ToString();
            DogNameTxt.Text = HealthGView.SelectedRows[0].Cells[2].Value.ToString();
            ReportDate.Text = HealthGView.SelectedRows[0].Cells[3].Value.ToString();
            EventTxt.Text   = HealthGView.SelectedRows[0].Cells[4].Value.ToString();
            DiagTxt.Text    = HealthGView.SelectedRows[0].Cells[5].Value.ToString();
            CostTxt.Text    = HealthGView.SelectedRows[0].Cells[6].Value.ToString();
            VetTxt.Text     = HealthGView.SelectedRows[0].Cells[7].Value.ToString();
            TreatTxt.Text   = HealthGView.SelectedRows[0].Cells[8].Value.ToString();
           
  
            if (DogNameTxt.Text == "")
            {
                key = 0;
                
            }
            else
            {
                key = Convert.ToInt32(HealthGView.SelectedRows[0].Cells[0].Value.ToString());
                
            }

        }

        private void label9_Click(object sender, EventArgs e)
        {

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
                    string Query = "delete from Health where ReportId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListHealth();
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
            if (DogIdBox.SelectedIndex == -1|| DogNameTxt.Text == "" || DiagTxt.Text == ""|| EventTxt.Text == ""|| TreatTxt.Text == ""|| CostTxt.Text == ""|| VetTxt.Text == "")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    DateTime date = Convert.ToDateTime(ReportDate.Value);
                    string Query = "update Health  set DogId="+DogIdBox.SelectedValue.ToString()+ " ,DogName=N'"+DogNameTxt.Text+"',ReportDate= N'"+date.ToString("MM-dd-yyyy")+"',Event= N'"+EventTxt.Text+"',Diagnosis= N'"+DiagTxt.Text+"',Cost= '"+CostTxt.Text+ "' ,Vetname= N'" + VetTxt.Text+"',Treatment= N'"+TreatTxt.Text+"' where ReportId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showListHealth();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }



        private void label8_Click_1(object sender, EventArgs e)
        {
            Dogs dogs = new Dogs();

            dogs.Show();
            this.Hide();
        }

        private void label9_Click_1(object sender, EventArgs e)
        {
            Health health = new Health();
            health.Show();
            this.Hide();
        }

        private void label10_Click_1(object sender, EventArgs e)
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
            Dashboard dsh = new Dashboard();
            dsh.Show();
            this.Hide();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new frmAbout().Show();
        }
    }
}
