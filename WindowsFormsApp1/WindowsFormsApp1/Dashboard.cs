using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        private void label10_Click(object sender, EventArgs e)
        {
            Breed breeding = new Breed();
            breeding.Show();
            this.Hide();
        }
    }
}
