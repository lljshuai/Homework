using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace work4._2
{
    public delegate void TickEventHandler(object a, TickEventArgs args);
    public class TickEventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public delegate void AlarmEventHandler(object b, AlarmEventArgs args);
    public class AlarmEventArgs
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Event
    {
        public event TickEventHandler TickEvent;
        public event AlarmEventHandler AlarmEvent;

        public void Tick(int x,int y)
        {
            
            TickEventArgs args = new TickEventArgs()
            {
                X = x,
                Y = y
            };

            TickEvent(this, args);
        }
        public void Alarm(int x, int y)
        {
            AlarmEventArgs args = new AlarmEventArgs()
            {
                X = x,
                Y = y
            };

            AlarmEvent(this, args);

        }

    }
    public class Form
    {
        public Event event1 = new Event();
        int x=0;int y=0;
        public Form()
        {
            event1.TickEvent += new TickEventHandler(Clock_Tick);
        }

        void Clock_Tick(object a, TickEventArgs args)
        {
            for (y=0; y <= 60; y++)
            {
                if (y == 60)
                {
                    y = 0; x = x + 1;
                    Console.WriteLine(x + ":" + y);
                    Thread.Sleep(500);
                }
                else if (x == 0 && y == 10)
                {
                    Clock_Alarm( a,  args);
                }
                else
                {
                    Console.WriteLine(x + ":" + y);

                    Thread.Sleep(500);
                }
            }
        }

        void Clock_Alarm(object a, TickEventArgs args)
        {
                    Console.WriteLine(x + ":" + y+"   闹钟响了！");
                    Thread.Sleep(500);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Form form1 = new Form();
            form1.event1.Tick(0, 0);//模拟点击按钮

        }
    }
}

