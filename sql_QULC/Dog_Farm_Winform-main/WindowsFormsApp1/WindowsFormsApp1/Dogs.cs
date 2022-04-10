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
        #region khai bao bien 
        int age = 0;
        int key = 0;
        SqlConnection conn = new SqlConnection(Properties.Settings.Default.strSQL);
        #endregion
        #region cac phuoc thuc chuyen form
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

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            Application.Exit();
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

        private void label12_Click_1(object sender, EventArgs e)
        {
            Dashboard dsh = new Dashboard();
            dsh.Show();
            this.Hide();

        }

        #endregion
        #region cac phuoc thuc 

        private void clearTxt()
        {

            DogNameTxt.Text = "";
            ColorTxt.Text = "";
            BreedTxt.Text = "";
            WeightTxt.Text = "";
            AgeTxt.Text = "";
            key = 0;

        }
        private void showListDog()
        {
            conn.Open();
            string Query = "SELECT TOP (1000) [DogId]N'Mã số' ,[DogName] N'Tên chó',[Color] N'Màu lông',[Breed] N'Giống chó' ,[Age] N'Tuổi',[Wei] N'Cân nặng',[Cost]N'Đơn giá'FROM[QLGC].[dbo].[Dog] ";
            SqlDataAdapter adapter = new SqlDataAdapter(Query, conn);
        SqlCommandBuilder cmd = new SqlCommandBuilder(adapter);
        var ds = new DataSet();
        adapter.Fill(ds);
            DogDGView.DataSource = ds.Tables[0];
            conn.Close();
        }
    #endregion
    #region cac su kien click
    private void button5_Click(object sender, EventArgs e)
    {
        clearTxt();
    }
    private void button1_Click(object sender, EventArgs e)
    {
        if (DogNameTxt.Text == "" || ColorTxt.Text == "" || BreedTxt.Text == "" || WeightTxt.Text == "" || AgeTxt.Text == "" || textBox_Cost.Text =="")
        {
            MessageBox.Show("Chua dien day du thong tin");
        }
        else
        {
            try
            {
                conn.Open();
                string Query = "insert into Dog values(N'" + DogNameTxt.Text + "',N'" + ColorTxt.Text + "',N'" + BreedTxt.Text + "',N'" + AgeTxt.Text + "',N'" + WeightTxt.Text + "',N'" + textBox_Cost.Text + "') ";
                SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Thanhcong");
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
        if (DogNameTxt.Text == "" || ColorTxt.Text == "" || BreedTxt.Text == "" || WeightTxt.Text == "" || AgeTxt.Text == "")
        {
            MessageBox.Show("Chua dien day du thong tin");
        }
        else
        {
            try
            {
                conn.Open();
                string Query = "update Dog  set DogName=N'" + DogNameTxt.Text + "',Color= N'" + ColorTxt.Text + "',Breed= N'" + BreedTxt.Text + "',Age= N'" + AgeTxt.Text + "',Wei= N'" + WeightTxt.Text + "',Cost= N'" + textBox_Cost.Text + "' where DogId =" + key + ";";
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


    private void DOBTxt_ValueChanged(object sender, EventArgs e)
    {
        age = Convert.ToInt32((DateTime.Today.Date - DOBTxt.Value.Date).Days) / 365;
        AgeTxt.Text = "" + age;
    }




    private void DogDGView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {

        DogNameTxt.Text = DogDGView.SelectedRows[0].Cells[1].Value.ToString();
        ColorTxt.Text = DogDGView.SelectedRows[0].Cells[2].Value.ToString();
        BreedTxt.Text = DogDGView.SelectedRows[0].Cells[3].Value.ToString();
        AgeTxt.Text = DogDGView.SelectedRows[0].Cells[4].Value.ToString();
        WeightTxt.Text = DogDGView.SelectedRows[0].Cells[5].Value.ToString();
        if (DogNameTxt.Text == "")
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

    #endregion

    private void SearchDog()
    {
        conn.Open();
        string Query = "select *from Dog Where Breed like '%" + DogSearchTxt.Text + "%'";
        SqlDataAdapter sda = new SqlDataAdapter(Query, conn);
        SqlCommandBuilder buider = new SqlCommandBuilder(sda);
        var ds = new DataSet();
        sda.Fill(ds);
        DogDGView.DataSource = ds.Tables[0];
        conn.Close();
    }

    private void DogSearchTxt_TextChanged(object sender, EventArgs e)
    {
        SearchDog();
    }

    private void label15_Click(object sender, EventArgs e)
    {
        Application.Exit();

    }

        

        private void CheckNhap(object sender, KeyPressEventArgs e)
        {
            checkNhap(e);
        }
        private void checkNhap(KeyPressEventArgs e)
        {
            //Char.IsDigit(e.KeyChar) –> kiểm tra xem phím vừa nhập vào textbox có phải là ký tự số hay không, hàm này trả về kiểu bool
            //Char.IsContro(e.KeyChar) –> kiểm tra xem phím vừa nhập vào textbox có phải là các ký tự điều khiển (các phím mũi tên,Delete,Insert,backspace,space bar) hay không, mục đích dùng hàm này là để cho phép người dùng xóa số trong trường hợp nhập sai.
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void CheckNhap_CN(object sender, KeyPressEventArgs e)
        {
            checkNhap(e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            new frmAbout().Show();
        }
    }
}
