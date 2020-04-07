using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace project5._1
{

    public enum Products
    {
        Rose, Lily, Lotus, Carnation, Violet, Lilac
    };
    [Serializable]

    public class OrderItems : IComparable //订单明细
    {
        public OrderItems() { }
        public Products Variety { get; set; }
        public int ProductsNum { get; set; }
        public int Price { get; set; }
        public int Sum { get; set; }
        public string OrderNum { set; get; }
        public string ClientName { set; get; }

        public OrderItems(string orderNum, string clientName, Products variety, int productsNum, int price)
        {
            Variety = variety;
            ProductsNum = productsNum;
            Price = price;
            OrderNum = orderNum;
            ClientName = clientName;
            Sum = productsNum * price;
        }
        public override string ToString()
        {
            return "订单号：" + OrderNum + "\n客户姓名：" + ClientName + "\n--------------------------" +
                   "\n鲜花类型：" + Variety + "\n单价: " + Price + "\n数量: " + ProductsNum +
                   "\n总价: " + Sum + "\n=====================================================";
        }
        public int CompareTo(object obj)
        {
            return Sum.CompareTo(obj);
        }

    }


    [Serializable]
    public class OrderService
    {
        public OrderService() { }

        public List<OrderItems> orders = new List<OrderItems>();//储存订单

        public  void AddOrder(OrderItems order) => orders.Add(order);//添加订单

        public  void RemoveOrder(OrderItems order)
        {
            orders.Remove(order);
        }//删除订单

        public  void ModifyClientName(OrderItems order, string name)//修改订单客户名
        {
            try
            {
                if (order == null)
                {
                    throw new DataException("订单不存在！");
                }
                if (name == null && string.Equals(name, ""))
                {
                    throw new DataException("客户名不存在！");
                }
                order.ClientName = name;

            }
            catch (DataException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }


        public  OrderItems FindOrderByOrderNum(string num)//根据订单号查询,返回订单对象
        {
            bool exit = false;
            foreach (var od in orders)
            {
                var query = from od1 in orders
                            where string.Equals(num, od1.OrderNum)
                            select od1;
                if (string.Equals(num, od.OrderNum))
                {
                    exit = true;
                }
                foreach (var od2 in query)
                {
                    return od2;
                }
            }
            if (exit == false)
            {
                throw new DataException("不存在该订单号的订单！");
            }
            return null;
        }

        public  OrderItems FindOrderByClientName(string name)//根据客户名查询，显示订单内容
        {
            bool exit = false;
            foreach (var od in orders)
            {
                var query = from od1 in orders
                            where string.Equals(name, od1.ClientName)
                            select od1;
                if (string.Equals(name, od.ClientName))
                {
                    exit = true;
                }
                foreach (var od2 in query)
                {
                    return od2;
                }
            }
            if (exit == false)
            {
                throw new DataException("不存在该客户的订单！");
            }
            return null;
        }

        public  OrderItems FindOrderByProductVariety(Products variety)//根据鲜花类型查询，显示订单内容
        {
            bool exit = false;
            foreach (var od in orders)
            {
                var query = from od1 in orders
                            where string.Equals(variety, od1.Variety)
                            select od1;
                if (string.Equals(variety, od.Variety))
                {
                    exit = true;
                }
                foreach (var od2 in query)
                {
                    return od2;
                }
            }
            if (exit == false)
            {
                throw new DataException("不存在该产品的订单！");
            }
            return null;
        }

        public  void Export(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OrderItems>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, orders);
            }
        }

        public  List<OrderItems> Import(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OrderItems>));
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                List<OrderItems> orders01 = (List<OrderItems>)xmlSerializer.Deserialize(fs);
                foreach (var od in orders01)
                {
                    Console.WriteLine(od);
                }
                return orders01;
            }
        }
    }




    class DataException : ApplicationException
    {
        public DataException(string message) : base(message)
        {

        }
    }

    class MyAppException : ApplicationException
    {
        public MyAppException(string message) : base(message)
        {

        }
        public MyAppException(string message, Exception inner) : base(message, inner)
        {

        }
    }


    class Program
    {
        //比较
        public class AgeComparer : IComparer<OrderItems>
        {
            public int Compare(OrderItems od1, OrderItems od2)
            {
                return od1.Sum - od2.Sum;
            }
        }
        static void Main(string[] args)
        {

            //订单明细
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderItems od04 = new OrderItems("20200317", "D", Products.Carnation, 42, 8);
            OrderItems od05 = new OrderItems("20200318", "E", Products.Lily, 100, 14);
            OrderItems od06 = new OrderItems("20200319", "F", Products.Lilac, 10, 20);

            OrderService orderService = new OrderService();
            orderService.AddOrder(od01);
            orderService.AddOrder(od02);
            orderService.AddOrder(od03);
            orderService.AddOrder(od04);
            orderService.AddOrder(od05);
            orderService.AddOrder(od06);
            
            Console.WriteLine("按总价排序：");
            //按总价排序
            orderService.orders.Sort((od1, od2) => od1.Sum - od2.Sum);
            orderService.orders.ForEach(od => Console.WriteLine(od));
            Console.WriteLine("\n\n按数目排序：");
            //按数目排序
            orderService.orders.Sort((od1, od2) => od1.ProductsNum - od2.ProductsNum);
            orderService.orders.ForEach(od => Console.WriteLine(od));
            
            //XML序列化和反序列化
            String s01 = "order.xml";
            orderService.Export(s01);
            Console.WriteLine(File.ReadAllText(s01));
            orderService.Import(s01);

            //查询订单               
            Console.WriteLine("请输入需要查询的类型：1.订单号 2.客户名 3.鲜花种类 ");
            int n = Convert.ToInt32(Console.ReadLine());
            if (n == 1)
            {
                Console.WriteLine("请输入查询订单号：");
                String ordernum01 = Console.ReadLine();
                try
                {
                    Console.WriteLine();
                    Console.WriteLine(orderService.FindOrderByOrderNum(ordernum01));
                }
                catch (DataException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else if (n == 2)
            {
                Console.WriteLine("请输入查询客户名称：");
                String clientname = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine( orderService.FindOrderByClientName(clientname));
            }
            else
            {
                Console.WriteLine("请输入查询鲜花种类：1.Rose 2.Lily 3.Lotus 4.Carnation 5.Violet 6.Lilac");
                int m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Rose));
                        break;
                    case 2:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Lily));
                        break;
                    case 3:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Lotus));
                        break;
                    case 4:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Carnation));
                        break;
                    case 5:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Violet));
                        break;
                    case 6:
                        Console.WriteLine();
                        Console.WriteLine(orderService.FindOrderByProductVariety(Products.Lilac));
                        break;
                }
            }
        }
        }
    }


