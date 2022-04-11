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
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
            showList();
        }
      
        #region khai bao bien
        int key = 0;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.strSQL);
        #endregion


        #region Chuyen form
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
            Dashboard dsh = new Dashboard();
            dsh.Show();
            this.Hide();
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
        #endregion

        private void showList()
        {
            conn.Open();
            string Query = "SELECT TOP (1000) [EmpId]N'Mã số'"+
   "   ,[EmpName]N'Họ và tên'" +
   "   ,[EmpDob] N'Ngày sinh'" +
   "   ,[Gender] N'Giới tính'"+
   "   ,[Phone] N'Số điện thoại'"+
   "   ,[Address] N'Địa chỉ'"+
 " FROM[QLGC].[dbo].[Employee]";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(Query, conn);
            SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(sqlDataAdapter);
            var ds = new DataSet();
            sqlDataAdapter.Fill(ds);
            EmpDGView.DataSource = ds.Tables[0];

            conn.Close();

        }

        private void clearTxt()
        {

            NameTxt.Text ="";
            PhoneTxt.Text = "";
            AddressTxt.Text = "";
            comboBoxGender.SelectedIndex = -1;
            key = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (comboBoxGender.SelectedIndex == -1|| NameTxt.Text == "" || PhoneTxt.Text == ""|| AddressTxt.Text == "")
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();

                    DateTime date = Convert.ToDateTime(DOBTxt.Value);
                    string Query = "insert into Employee  values(N'"+NameTxt.Text+"','"+date.ToString("MM-dd-yyyy")+"' ,N'"+comboBoxGender.SelectedItem.ToString()+"','"+PhoneTxt.Text+ "' ,N'"+AddressTxt.Text+"')";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showList();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            clearTxt();
        }

        private void EmpDGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            NameTxt.Text                    = EmpDGView.SelectedRows[0].Cells[1].Value.ToString();
            DOBTxt.Text                     = EmpDGView.SelectedRows[0].Cells[2].Value.ToString();
            comboBoxGender.SelectedItem     = EmpDGView.SelectedRows[0].Cells[3].Value.ToString();
            PhoneTxt.Text                   = EmpDGView.SelectedRows[0].Cells[4].Value.ToString();
            AddressTxt.Text                 = EmpDGView.SelectedRows[0].Cells[5].Value.ToString();
            if (NameTxt.Text == "")
            {
                key = 0;

            }
            else
            {
                key = Convert.ToInt32(EmpDGView.SelectedRows[0].Cells[0].Value.ToString());

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Chon mot nhan vien cho de xoa");
            }
            else
            {
                try
                {
                    conn.Open();
                    string Query = "delete from Employee where EmpId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showList();
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

            if (comboBoxGender.SelectedIndex == -1|| NameTxt.Text == "" || PhoneTxt.Text == ""|| AddressTxt.Text == "" || key == 0)
            {
                MessageBox.Show("Chua dien day du thong tin");
            }
            else
            {
                try
                {
                    conn.Open();
                    DateTime date = Convert.ToDateTime(DOBTxt.Value);
                    string Query = "update Employee  set EmpName=N'"+NameTxt.Text+ "' ,EmpDob= '"+date.ToString("MM-dd-yyyy")+"',Gender= N'"+comboBoxGender.SelectedItem.ToString()+"',Phone= '"+PhoneTxt.Text+"',Address= N'"+AddressTxt.Text+ "' where EmpId =" + key + ";";
                    SqlCommand cmd = new SqlCommand(Query, conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thanh cong");
                    conn.Close();

                    showList();
                    clearTxt();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new frmAbout().Show();
        }
    }
}
