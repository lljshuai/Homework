using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work01
{
    class Program
    {
        static void Main(string[] args)
        {
            double [] array;
            array = new double [10];
            double sum = 0;
            for (int i = 0; i <10; i++)
            {
                Console.WriteLine("请输入形状：1.长方形 2.三角形 3.正方形");
                int n = Convert.ToInt32(Console.ReadLine());
                Shape shape = Factory.CreateFunction(n);
                if (shape != null)
                {
                    Console.WriteLine("面积为：" + shape.Area());
                }
                array[i] = shape.Area();
                sum += array[i];
                Console.WriteLine("前"+(i+1)+"个图像总面积为：" + sum);
            }
        }
    }
}
