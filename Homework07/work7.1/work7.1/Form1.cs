using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace work7._1
{
    public partial class Form1 : Form
    {        
        private Graphics graphics;        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = this.trackBar1.Value;
            this.label2.Text = n.ToString();
            int m = this.trackBar2.Value;
            this.label5.Text = m.ToString();
            if (graphics == null) graphics = this.CreateGraphics();
            drawCayleyTree(n, 200, 310, m, -Math.PI / 2);
        }
        void drawCayleyTree(int n, double x0, double y0, double leng, double th)
        {
            if (n == 0) return;
            double x1 = x0 + leng * Math.Cos(th);
            double y1 = y0 + leng * Math.Sin(th);

            drawLine(x0, y0, x1, y1);
            double per1 = double.Parse(this.domainUpDown1.Text);
            double per2 = double.Parse(this.domainUpDown2.Text);
            double th1 = double.Parse(this.textBox1.Text)* Math.PI / 180;
            double th2 = double.Parse(this.textBox2.Text) * Math.PI / 180;

            drawCayleyTree(n - 1, x1, y1, per1 * leng, th + th1);
            drawCayleyTree(n - 1, x1, y1, per2 * leng, th - th2);

        }
        void drawLine(double x0, double y0, double x1, double y1)
        {
            String color= this.comboBox1.SelectedItem.ToString();
            switch (color)
            {
                case "yellow":graphics.DrawLine(Pens.Yellow, (int)x0, (int)y0, (int)x1, (int)y1);break;
                case "red": graphics.DrawLine(Pens.Red, (int)x0, (int)y0, (int)x1, (int)y1); break;
                case "black": graphics.DrawLine(Pens.Black, (int)x0, (int)y0, (int)x1, (int)y1); break;
                case "blue": graphics.DrawLine(Pens.Blue, (int)x0, (int)y0, (int)x1, (int)y1); break;
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar1.Maximum = 20;
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            trackBar2.Maximum = 100;
            trackBar2.Minimum = 50;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            g.Clear(this.BackColor);
            g.Dispose(); 
        }

    }
}
