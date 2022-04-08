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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-SCBOTSE\SQLEXPRESS;Initial Catalog=QLGC;Integrated Security=True");

        private void label4_Click(object sender, EventArgs e)
        {
            usernametxt.Text = "";
            passwordtxt.Text ="";

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (passwordtxt.Text == ""|| usernametxt.Text=="")
            {
                MessageBox.Show("Nhap thong tin ");
            }
            else
            {
                if (roletxt.SelectedIndex > -1)
                {

                    if (roletxt.SelectedItem.ToString()=="Admin")
                    {
                        if (usernametxt.Text=="Admin"&& passwordtxt.Text=="Admin")
                        {
                            Employee emp = new Employee();
                            emp.Show();
                            this.Hide();

                        }
                        else
                        {
                            MessageBox.Show("Sai ten hoac mat khau cua admin");

                        }
                    }
                    else
                    {
                        conn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) from Employee where EmpName ='"+usernametxt+"'and EmpPass='"+passwordtxt+ "' ", conn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString()=="1")
                        {
                            Dogs dog = new Dogs();
                            dog.Show();
                            this.Hide();
                            conn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Sai thong tin");
                        }
                        conn.Close();
                    }

                }
                else
                {
                    MessageBox.Show("Chon role");
                }

            }
        }

    }
}
