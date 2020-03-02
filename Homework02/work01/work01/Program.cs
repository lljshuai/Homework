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
            
            Console.WriteLine("请输入一个数：");
            int n = 0;int i = 1;
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(n + "以内的素数有：");
            bool s; 
            for (; i < n ; i++)

            {
                s = true;
                for (int j = 2; j < i; j++)

                {
                    if (i % j == 0)
                        s = false;
                }
                if (s)
                    Console.WriteLine(i.ToString());
            }
        }
    }
}
