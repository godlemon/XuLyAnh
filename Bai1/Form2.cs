using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bai1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
            
        }
        public double ayl()
        {
            double a = Convert.ToDouble(ay.Text);
            return a;
        }
        public double byl()
        {
            double b = Convert.ToDouble(bi.Text);
            return b;
        }
        public double Ayl()
        {
            double A = Convert.ToDouble(gam.Text); return A;
        }
        public double Byl()
        {
            double B = Convert.ToDouble(al.Text); return B;
        }
        public double Gyl()
        {
            double G = Convert.ToDouble(be.Text); return G;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
