using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace work4._1
{

    // 链表节点
    // 链表节点
    public class Node<T>
    {
        public Node<T> Next { get; set; }
        public T Data { get; set; }

        public Node(T t)
        {
            Next = null;
            Data = t;
        }
    }

    //泛型链表类
    public class GenericList<T>
    {
        private Node<T> head;
        private Node<T> tail;

        public GenericList()
        {
            tail = head = null;
        }

        public Node<T> Head
        {
            get => head;
        }

        public void Add(T t)
        {
            Node<T> n = new Node<T>(t);
            if (tail == null)
            {
                head = tail = n;
            }
            else
            {
                tail.Next = n;
                tail = n;
            }
        }

        public void Foreach(Action<T> action)
        {
            Node<T> n = (head == null) ? null : head;
            action(n.Data);
            while (n != null)
            {
                action(n.Data);
                n = n.Next;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0, max = 0, min = 0;
            GenericList<int> list = new GenericList<int>();
            for (int x = 0; x < 10; x++)
            {
                list.Add(x);
            }
            list.Foreach(x => sum += x);
            list.Foreach(x => Console.WriteLine(x));
            list.Foreach(x => max = (max > x) ? max : x);
            list.Foreach(x => min = (min > x) ? x : min);
            Console.WriteLine("最大值为："+max);
            Console.WriteLine("最小值为："+min);
        }
    }
}
