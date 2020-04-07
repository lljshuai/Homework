using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using project5._1;

namespace OrderServiceWinForm
{
    public partial class Form1 : Form
    {            
        public string KeyWord { get; set; }
        public OrderService orderService = new OrderService();
        public List<OrderItems> ods = new List<OrderItems>();
        Form2 form2 = new Form2();


        public Form1()
        {
            InitializeComponent();
            queryInput.DataBindings.Add("Text", this, "KeyWord");
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderItems od04 = new OrderItems("20200317", "D", Products.Carnation, 42, 8);
            OrderItems od05 = new OrderItems("20200318", "E", Products.Lily, 100, 14);
            OrderItems od06 = new OrderItems("20200319", "F", Products.Lilac, 10, 20);

            orderService.AddOrder(od01);
            orderService.AddOrder(od02);
            orderService.AddOrder(od03);
            orderService.AddOrder(od04);
            orderService.AddOrder(od05);
            orderService.AddOrder(od06);
            ods = orderService.orders;
        }

        private void button1_Click(object sender, EventArgs e)//查询按钮
        {
            try
            {
                if (radioButton1.Checked==true)
                {
                    orderItemsBindingSource.DataSource = orderService.FindOrderByOrderNum(KeyWord);
                }
                else if (radioButton2.Checked == true)
                {
                    orderItemsBindingSource.DataSource = orderService.FindOrderByClientName(KeyWord);
                }
                else if (radioButton3.Checked == true)
                {
                    orderItemsBindingSource.DataSource = orderService.FindOrderByProductVariety((Products)Enum.Parse(typeof(Products),
                        KeyWord.ToString()));
                }
            }
            catch (DataException ev)
            {
                MessageBox.Show(ev.Message);
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)//菜单栏
        {
            form2.Show();
        }


        private void button2_Click(object sender, EventArgs e)//刷新按钮
        {
            orderItemsBindingSource.DataSource = null;
            if (form2.ADD == true)
            {
                form2.ADD = false;
                Products products = Products.Rose;
                int m = 0;
                switch (form2.comboBox1.Text)
                {
                    case "Rose":
                        products = Products.Rose; m = 12;
                        break;
                    case "Carnation":
                        products = Products.Carnation; m = 8;
                        break;
                    case "Lilac":
                        products = Products.Lilac; m = 20;
                        break;
                    case "Lily":
                        products = Products.Lily; m = 14;
                        break;
                    case "Lotus":
                        products = Products.Lotus; m = 5;
                        break;
                    case "Violet":
                        products = Products.Violet; m = 10;
                        break;
                }
                OrderItems order = new OrderItems(form2.ordernum, form2.clientname, products, form2.productnum, m);
                ods.Add(order);
            }
            orderItemsBindingSource.DataSource = ods;
        }

        private void button3_Click(object sender, EventArgs e)//显示订单按钮
        {
            orderItemsBindingSource.DataSource = ods;
        }

        private void button4_Click(object sender, EventArgs e)//删除按钮
        {
            foreach (DataGridViewRow row in this.dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dataGridView1.Rows.Remove(row);
                }
            }
        }
    }
}
