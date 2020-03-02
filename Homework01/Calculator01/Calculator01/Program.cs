using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Declare variables and then initialize to zero.
                float num2 = 0; float num1 = 0;
                try
                {
                    // Ask the user to type the first number.
                    Console.WriteLine("输入第一个运算数：");
                    num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("------------------------");
                }
                catch (FormatException)
                {
                    Console.WriteLine("非法字符，请输入正确的数字：");
                    num1 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("------------------------");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("数据过大！！");
                    num1 = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("------------------------");
                }


                try
                {
                    // Ask the user to type the first number.
                    Console.WriteLine("输入第二个运算数：");
                    num2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("------------------------");
                }
                catch (FormatException)
                {
                    Console.WriteLine("非法字符，请输入正确的数字：");
                    num2 = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("------------------------");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("数据过大！！");
                    num2 = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("------------------------");

                }

                Console.Write("请输入运算符 + - * / ：");

                // Use a switch statement to do the math.
                switch (Console.ReadLine())
                {
                    case "+":
                        Console.WriteLine($"结果为: {num1} + {num2} = " + (num1 + num2));
                        break;
                    case "-":
                        Console.WriteLine($"结果为: {num1} - {num2} = " + (num1 - num2));
                        break;
                    case "*":
                        try
                        {
                            Console.WriteLine($"结果为: {num1} * {num2} = " + (num1 * num2));
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("数据过大！！");
                        }

                        break;
                    case "/":
                        while (num2 == 0)
                        {
                            Console.WriteLine("请输入非零数字：");
                            num2 = Convert.ToInt32(Console.ReadLine());
                        }
                        Console.WriteLine($"结果为: {num1} / {num2} = " + (num1 / num2));
                        break;
                }
                Console.WriteLine("------------------------");
                Console.WriteLine("------------------------");
                Console.WriteLine();
            }
        }
    }
}