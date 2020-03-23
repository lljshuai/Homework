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
        internal Products Variety { get; set; }
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

        public static List<OrderItems> orders;//储存订单

        public static void AddOrder(OrderItems order) => orders.Add(order);//添加订单

        public static void RemoveOrder(OrderItems order)
        {
            orders.Remove(order);
        }//删除订单

        public static void ModifyClientName(OrderItems order, string name)//修改订单客户名
        {
            try
            {
                if (order == null)
                {
                    throw new DataException("订单不存在！");
                }
                if(name==null&&string.Equals(name, ""))
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


        public static OrderItems FindOrderByOrderNum(string num)//根据订单号查询,返回订单对象
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

        public  static OrderItems FindOrderByClientName(string name)//根据客户名查询，显示订单内容
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

        public static OrderItems FindOrderByProductVariety(Products variety)//根据鲜花类型查询，显示订单内容
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

        public static void Export(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OrderItems>));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, OrderService.orders);
            }
        }

        public static List<OrderItems> Import(string path)
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

            
            //订单服务,添加订单
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            OrderService.AddOrder(od04);
            OrderService.AddOrder(od05);
            OrderService.AddOrder(od06);
            /*
            Console.WriteLine("按总价排序：");
            //按总价排序
            OrderService.orders.Sort((od1, od2) => od1.Sum - od2.Sum);
            OrderService.orders.ForEach(od => Console.WriteLine(od));
            Console.WriteLine("\n\n按数目排序：");
            //按数目排序
            OrderService.orders.Sort((od1, od2) => od1.ProductsNum - od2.ProductsNum);
            OrderService.orders.ForEach(od => Console.WriteLine(od));
            */

            //XML序列化和反序列化
            String s01 = "order.xml";
            OrderService.Export(s01);
            Console.WriteLine(File.ReadAllText(s01));
            OrderService.Import(s01);

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
                    Console.WriteLine(OrderService.FindOrderByOrderNum(ordernum01));
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
                OrderService.FindOrderByClientName(clientname);
            }
            else
            {
                Console.WriteLine("请输入查询鲜花种类：1.Rose 2.Lily 3.Lotus 4.Carnation 5.Violet 6.Lilac");
                int m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1: OrderService.FindOrderByProductVariety(Products.Rose); break;
                    case 2: OrderService.FindOrderByProductVariety(Products.Lily); break;
                    case 3: OrderService.FindOrderByProductVariety(Products.Lotus); break;
                    case 4: OrderService.FindOrderByProductVariety(Products.Carnation); break;
                    case 5: OrderService.FindOrderByProductVariety(Products.Violet); break;
                    case 6: OrderService.FindOrderByProductVariety(Products.Lilac); break;
                }
            }
        }
    }
}

