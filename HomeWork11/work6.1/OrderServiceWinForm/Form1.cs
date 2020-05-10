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
        public OrderContext ods01 = new OrderContext();
        public List<OrderItems> ods = new List<OrderItems>();
        Form2 form2 = new Form2();


        public Form1()
        {
            InitializeComponent();
            queryInput.DataBindings.Add("Text", this, "KeyWord");
            foreach (var odit in ods01.OrderItems)
            {
                ods.Add(odit);
            }
            orderItemsBindingSource.DataSource = ods;
        }

        private void button1_Click(object sender, EventArgs e)//查询按钮
        {
            try
            {
                if (radioButton1.Checked == true)
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
                orderService.AddOrder(order);
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
                    OrderItems order = orderItemsBindingSource.Current as OrderItems;
                    ods01.OrderItems.Remove(order);
                    ods01.SaveChanges();
                    this.dataGridView1.Rows.Remove(row);
                }
            }
        }
    }
}
