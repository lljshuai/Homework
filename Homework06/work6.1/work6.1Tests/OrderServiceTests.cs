using Microsoft.VisualStudio.TestTools.UnitTesting;
using project5._1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project5._1.Tests
{
    [TestClass()]
    public class OrderServiceTests
    {
        
        [TestMethod()]
        public void AddOrderTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            //添加空订单
            OrderItems od02 = new OrderItems();
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od02);
        }

        [TestMethod()]
        public void RemoveOrderTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.RemoveOrder(od01);
            Assert.IsTrue(OrderService.orders.Count == 0);
        }

        [TestMethod()]
        public void ModifyClientNameTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            OrderService.ModifyClientName(od01, "M");
            Assert.AreEqual(od01.ClientName, "M");
            OrderService.ModifyClientName(od02, "");
            OrderService.ModifyClientName(od03, null);
        }

        [TestMethod()]
        public void FindOrderByOrderNumTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            List<OrderItems> orders02 = new List<OrderItems>()
            {
                OrderService.FindOrderByOrderNum("20200315")
            };
            Assert.AreEqual(orders02.Count, 1);
        }

        [TestMethod()]
        public void FindOrderByClientNameTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            List<OrderItems> orders02 = new List<OrderItems>()
            {
                OrderService.FindOrderByClientName("A")
            };
            Assert.AreEqual(orders02.Count, 1);

        }

        [TestMethod()]
        public void FindOrderByProductVarietyTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            List<OrderItems> orders02 = new List<OrderItems>()
            {
                OrderService.FindOrderByProductVariety(Products.Rose)
            };
            Assert.AreEqual(orders02.Count, 1);
        }

        [TestMethod()]
        public void ExportTest()
        {
            OrderItems od01 = new OrderItems("20200314", "A", Products.Violet, 32, 10);
            OrderItems od02 = new OrderItems("20200315", "B", Products.Rose, 24, 12);
            OrderItems od03 = new OrderItems("20200316", "C", Products.Lotus, 18, 5);
            OrderService.orders = new List<OrderItems>();
            OrderService.AddOrder(od01);
            OrderService.AddOrder(od02);
            OrderService.AddOrder(od03);
            String s = "test.xml";
            OrderService.Export(s);
            System.IO.FileInfo file = new System.IO.FileInfo(s);
            Assert.IsTrue(file.Exists);

        }

        [TestMethod()]
        public void ImportTest()
        {
            String s = "test.xml";
            List<OrderItems> orders02 = new List<OrderItems>();
            orders02 = OrderService.Import(s);
            Assert.AreEqual(orders02.Count, 3);

        }
    }
}