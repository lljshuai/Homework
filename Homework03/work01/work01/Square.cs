using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work01
{
    //正方形
    class Square : Shape
    {
        double width;
        public Square()
        {
            Initialization();
        }
        public override double Area()
        {
            return Math.Pow(width, 2);
        }

        public override void Initialization()
        {
            while (true)
            {
                Console.WriteLine("请输入正方形的边长：");
                string widthStr = Console.ReadLine();
                if (!double.TryParse(widthStr, out width))
                {
                    Console.Write("输入数据出现非法字符，");
                    continue;
                }
                break;
            }
        }
    }
}
