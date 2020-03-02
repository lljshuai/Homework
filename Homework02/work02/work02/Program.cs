using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work02
{
    class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            Console.WriteLine("请输入数组长度：");
            num = Convert.ToInt32(Console.ReadLine());
            int[] array;
            array = new int[num];
            for(int i = 0; i < num; i++)
            {
                Console.WriteLine("请输入array[" + i + "]的值：");
                array[i] = Convert.ToInt32(Console.ReadLine());
            }
            int max, min, sum;
            max = min = sum = array[0];
            for(int i = 1; i < num; i++)
            {
                if (max < array[i]) max = array[i];
                if (min > array[i]) min = array[i];
                sum = sum + array[i];
            }
            float average = sum / num;
            Console.WriteLine("最大值为：" + max.ToString());
            Console.WriteLine("最小值为：" + min.ToString());
            Console.WriteLine("和为：" + sum.ToString());
            Console.WriteLine("平均值为：" + average .ToString());
        }
    }
}
