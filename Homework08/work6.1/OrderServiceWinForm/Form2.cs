using project5._1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrderServiceWinForm
{
    public partial class Form2 : Form
    {

        public bool ADD = false;
        public OrderItems order;
        public String clientname { get; set; }
        public String ordernum { get; set; }
        public int productnum { get; set; }
        public Products products { get; set; }

        public Form2()
        {
            InitializeComponent();
            
            /*textBox1.DataBindings.Add("Text", this, "ordernum");
            textBox2.DataBindings.Add("Text", this, "clientname");
            textBox3.DataBindings.Add("Text", this, "productnum");
            comboBox1.DataBindings.Add("Text", this, "products");
            */
        }

        public void button1_Click(object sender, EventArgs e)//添加按钮，并将数据清空
        {
            ADD = true;
            ordernum = textBox1.Text;
            clientname = textBox2.Text;
            productnum = int.Parse(textBox3.Text);
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            this.Hide();
        }
    }
}
