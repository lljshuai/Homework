using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator02
{
    public partial class Form1 : Form
    {
        public object Int { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "+";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入数字后点击+号", "提示", MessageBoxButtons.OK);
            }
            else
            {
                double r1 = Convert.ToDouble(textBox1.Text);
                double r2 = Convert.ToDouble(textBox2.Text);
                textBox3.Text = (Math.Round(r1, 2) + Math.Round(r2, 2)).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "-";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入数字后点击-号", "提示", MessageBoxButtons.OK);
            }
            else
            {
                double r1 = Convert.ToDouble(textBox1.Text);
                double r2 = Convert.ToDouble(textBox2.Text);
                textBox3.Text = (Math.Round(r1, 2) - Math.Round(r2, 2)).ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "*";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入数字后点击*号", "提示", MessageBoxButtons.OK);
            }
            else
            {
                double r1 = Convert.ToDouble(textBox1.Text);
                double r2 = Convert.ToDouble(textBox2.Text);
                textBox3.Text = (Math.Round(r1, 2) * Math.Round(r2, 2)).ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "/";
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("请输入数字后点击/号", "提示", MessageBoxButtons.OK);
            }
            else
            {
                double r1 = Convert.ToDouble(textBox1.Text);
                double r2 = Convert.ToDouble(textBox2.Text);
                textBox3.Text = (Math.Round(r1, 2) / Math.Round(r2, 2)).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "？";
            textBox1.Text = null;
            textBox2.Text = null;
            textBox3.Text = null;
        }
    }
}
