using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work01
{

    //长方形
    class Rectangle : Shape
    {
        double width;
        double height;
        public Rectangle()
        {
            Initialization();
        }

        public override double Area()
        {
            return width * height;
        }

        public override void Initialization()
        {
            while (true)
            {
                Console.WriteLine("请选择输入长方形的长：");
                string widthStr = Console.ReadLine();
                Console.WriteLine("请选择输入长方形的宽：");
                string heightStr = Console.ReadLine();
                if (!double.TryParse(widthStr, out width) || !double.TryParse(heightStr, out height))
                {
                    Console.Write("输入数据出现非法字符，");
                    continue;
                }
                break;
            }
        }
    }
}
