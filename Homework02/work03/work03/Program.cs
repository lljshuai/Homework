using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work03
{
    class Program
    {
        static void Main(string[] args)
        {
            int temp = 0;
            Console.WriteLine("2到100以内的质数有：");
            Console.WriteLine(2);
            for (int a = 3; a <= 100; a++)
            {

                for (int b = 2; b < 11; b++)
                {
                    temp = b;
                    if (a % b == 0)
                    {
                        break;
                    }
                }
                if (temp == 10)
                    Console.WriteLine(a);

            }
        }
    }
}
