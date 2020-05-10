using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Xml.Xsl;

//修改之前做的订单程序，基于EF框架，将订单保存到MySql数据库中，并实现订单的增删改查功能。
namespace project5._1
{

    public enum Products
    {
        Rose, Lily, Lotus, Carnation, Violet, Lilac
    };
    [Serializable]
    public class OrderContext : DbContext
    {
        //这里这个类是用来建立和数据库的联系
        public OrderContext() : base("OrderDataBase")
        {
            Database.SetInitializer(
                new DropCreateDatabaseIfModelChanges<OrderContext>());//这是创建连接的函数，按格式写就行
        }
        public DbSet<OrderItems> OrderItems { get; set; }
    }


    public class OrderItems : IComparable //订单明细
    {
        [Key]
        public string OrderNum { set; get; }
        [Required]
        public int ProductsNum { get; set; }
        [Required]

        public int Price { get; set; }
        [Required]

        public int Sum { get; set; }
        [Required]
        public Products Variety { get; set; }

        [Required]

        public string ClientName { set; get; }
        public OrderItems() { }
        public OrderItems(string orderNum, string clientName, Products variety, int productsNum, int price)
        {
            Variety = variety;
            ProductsNum = productsNum;
            Price = price;
            OrderNum = orderNum + productsNum.ToString();
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

        //public List<OrderItems> orders = new List<OrderItems>();//储存订单

        public void AddOrder(OrderItems order)
        {
            using (var db = new OrderContext())
            {
                db.OrderItems.Add(order);
                db.SaveChanges();
            }
        }
        //添加订单

        public void RemoveOrder(OrderItems order)
        {
            using (var db = new OrderContext())
            {
                db.OrderItems.Remove(order);
                db.SaveChanges();
            }
        }//删除订单

        public void ModifyClientName(OrderItems order, string name)//修改订单客户名
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


        public OrderItems FindOrderByOrderNum(string num)//根据订单号查询,返回订单对象
        {
            using (var db = new OrderContext())
            {
                List<OrderItems> order = db.OrderItems.Where(o => o.OrderNum.Equals(num)).ToList<OrderItems>();
                foreach (var od in order)
                {
                    return od;
                }
            }
            return null;

            //bool exit = false;
            //foreach (var od in orders)
            //{
            //    var query = from od1 in orders
            //                where string.Equals(num, od1.OrderNum)
            //                select od1;
            //    if (string.Equals(num, od.OrderNum))
            //    {
            //        exit = true;
            //    }
            //    foreach (var od2 in query)
            //    {
            //        return od2;
            //    }
            //}
            //if (exit == false)
            //{
            //    throw new DataException("不存在该订单号的订单！");
            //}
            //return null;
        }

        public OrderItems FindOrderByClientName(string name)//根据客户名查询，显示订单内容
        {
            using (var db = new OrderContext())
            {
                List<OrderItems> order = db.OrderItems.Where(o => o.ClientName.Equals(name)).ToList<OrderItems>();
                foreach (var od in order)
                {
                    return od;
                }
            }
            return null;
            //bool exit = false;
            //foreach (var od in orders)
            //{
            //    var query = from od1 in orders
            //                where string.Equals(name, od1.ClientName)
            //                select od1;
            //    if (string.Equals(name, od.ClientName))
            //    {
            //        exit = true;
            //    }
            //    foreach (var od2 in query)
            //    {
            //        return od2;
            //    }
            //}
            //if (exit == false)
            //{
            //    throw new DataException("不存在该客户的订单！");
            //}
            //return null;
        }

        public OrderItems FindOrderByProductVariety(Products variety)//根据鲜花类型查询，显示订单内容
        {
            using (var db = new OrderContext())
            {
                List<OrderItems> order = db.OrderItems.Where(o => o.Variety.Equals(variety)).ToList<OrderItems>();
                foreach (var od in order)
                {
                    return od;
                }
            }
            return null;
            //bool exit = false;
            //foreach (var od in orders)
            //{
            //    var query = from od1 in orders
            //                where string.Equals(variety, od1.Variety)
            //                select od1;
            //    if (string.Equals(variety, od.Variety))
            //    {
            //        exit = true;
            //    }
            //    foreach (var od2 in query)
            //    {
            //        return od2;
            //    }
            //}
            //if (exit == false)
            //{
            //    throw new DataException("不存在该产品的订单！");
            //}
        }

        //public  void Export(string path)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OrderItems>));
        //    using (FileStream fs = new FileStream(path, FileMode.Create))
        //    {
        //        xmlSerializer.Serialize(fs, orders);
        //    }
        //}

        //public  List<OrderItems> Import(string path)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<OrderItems>));
        //    using (FileStream fs = new FileStream(path, FileMode.Open))
        //    {
        //        List<OrderItems> orders01 = (List<OrderItems>)xmlSerializer.Deserialize(fs);
        //        foreach (var od in orders01)
        //        {
        //            Console.WriteLine(od);
        //        }
        //        return orders01;
        //    }
        //}
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
            OrderService orderService = new OrderService();
            var db = new OrderContext();
            if (db.OrderItems.Count()==0)
            {
                //订单明细
                OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
                OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
                OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
                OrderItems od04 = new OrderItems("20200317", "D", Products.Carnation, 42, 8);
                OrderItems od05 = new OrderItems("20200318", "E", Products.Lily, 100, 14);
                OrderItems od06 = new OrderItems("20200319", "F", Products.Lilac, 10, 20);

                orderService.AddOrder(od01);
                orderService.AddOrder(od02);
                orderService.AddOrder(od03);
                orderService.AddOrder(od04);
                orderService.AddOrder(od05);
                orderService.AddOrder(od06);
            }

            //Console.WriteLine("按总价排序：");
            ////按总价排序
            //orderService.orders.Sort((od1, od2) => od1.Sum - od2.Sum);
            //orderService.orders.ForEach(od => Console.WriteLine(od));
            //Console.WriteLine("\n\n按数目排序：");
            ////按数目排序
            //orderService.orders.Sort((od1, od2) => od1.ProductsNum - od2.ProductsNum);
            //orderService.orders.ForEach(od => Console.WriteLine(od));

            ////XML序列化和反序列化
            //String s01 = "order.xml";
            //orderService.Export(s01);
            //Console.WriteLine(File.ReadAllText(s01));
            //orderService.Import(s01);
            Console.WriteLine("请输入需要的服务：1.添加订单 2.查询订单");
            int n0 = Convert.ToInt32(Console.ReadLine());
            if (n0 == 1)
            {
                string orderNum;
                string clientName;
                Products variety = Products.Rose;
                int productsNum;
                int m0 = 0;
                //添加订单
                Console.WriteLine("请输入日期：");
                orderNum = Console.ReadLine();
                Console.WriteLine("请输入姓名：");
                clientName = Console.ReadLine();
                Console.WriteLine("请输入添加的鲜花种类：1.Rose 2.Lily 3.Lotus 4.Carnation 5.Violet 6.Lilac");
                int m = Convert.ToInt32(Console.ReadLine());
                switch (m)
                {
                    case 1:
                        variety = Products.Rose;m0 = 12;
                        break;
                    case 2:
                        variety = Products.Lily;m0 = 14;
                        break;
                    case 3:
                        variety = Products.Lotus;m0 = 5;
                        break;
                    case 4:
                        variety = Products.Carnation;m0 = 8;
                        break;
                    case 5:
                        variety = Products.Violet;m0 = 10;
                        break;
                    case 6:
                        variety = Products.Lilac;m0 = 20;
                        break;
                }
                Console.WriteLine("请输入数量：");
                productsNum = Convert.ToInt32(Console.ReadLine());
                OrderItems order = new OrderItems(orderNum, clientName, variety, productsNum, m0);
                orderService.AddOrder(order);
            }
            else if (n0 == 2)
            {
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
                    Console.WriteLine(orderService.FindOrderByClientName(clientname));
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
            else
            {
                Console.WriteLine("错误输入！");
            }
        }
    }
}


