using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work01
{
    class Triangle : Shape
    {
        double height;
        double bottom;
        public Triangle()
        {
            Initialization();
        }

        public override double Area()
        {
            return bottom * height / 2;
        }

        public override void Initialization()
        {
            while (true)
            {
                Console.Write("请输入三角形的底：");
                string bottomStr = Console.ReadLine();
                Console.Write("请输入三角形的高：");
                string heightStr = Console.ReadLine();
                if (!double.TryParse(bottomStr, out bottom) || !double.TryParse(heightStr, out height))
                {
                    Console.WriteLine("您输入的数据出现非法字符，请重新输入！");
                    continue;
                }
                break;
            }
        }
    }
}
