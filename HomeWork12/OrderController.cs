using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace OrderApp {

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {

        private readonly OrderContext orderDb;

       
        public TodoController(OrderContext context)
        {
            this.orderDb = context;
        }

        [HttpGet("Order")]
        public ActionResult<Order> GetOrder(long id)
        {
            var order = orderDb.Orders.FirstOrDefault(t => t.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return order;
        }
        [HttpGet("Customer")]
        public ActionResult<Customer> GetCustomer(long id)
        {
            var customer = orderDb.Customers.FirstOrDefault(t => t.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }
        [HttpGet("Goods")]
        public ActionResult<Goods> GetGoods(long id)
        {
            var goods = orderDb.GoodItems.FirstOrDefault(t => t.Id == id);
            if (goods == null)
            {
                return NotFound();
            }
            return goods;
        }
        [HttpGet("OrderItems")]
        public ActionResult<OrderItem> GetOrderItem(long id)
        {
            var orderitem = orderDb.OrderItems.FirstOrDefault(t => t.Id == id);
            if (orderitem == null)
            {
                return NotFound();
            }
            return orderitem;
        }
        [HttpGet("orderitemQuery")]
        public ActionResult<List<OrderItem>> queryTodoItem(string name , int skip, int take)
        {
            var query = buildQuery(name).Skip(skip).Take(take);
            return query.ToList();
        }

        private IQueryable<OrderItem> buildQuery(string name)
        {
            IQueryable<OrderItem> query = orderDb.OrderItems;
            if (name != null)
            {
                query = query.Where(t => t.Name.Contains(name));
            }
           
            return query;
        }
        [HttpPost("Order")]
        public ActionResult<Order> PostTodoItem(Order order)
        {
            try
            {
                orderDb.Orders.Add(order);
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return order;

        }

        [HttpPost("Customer")]
        public ActionResult<Customer> PostCustomer(Customer customer)
        {
            try
            {
                orderDb.Customers.Add(customer);
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return customer;
        }
        [HttpPost("Goods")]
        public ActionResult<Goods> PostGoods(Goods goods)
        {
            try
            {
                orderDb.GoodItems.Add(goods);
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return goods;
        }
        [HttpPut("Order")]
        public ActionResult<Order> PutOrder(long id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                orderDb.Entry(order).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }

        [HttpPut("Customer")]
        public ActionResult<Customer> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                orderDb.Entry(customer).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }
        [HttpPut("Goods")]
        public ActionResult<Goods> PutGoodsItem(long id, Goods goods)
        {
            if (id != goods.Id)
            {
                return BadRequest("Id cannot be modified!");
            }
            try
            {
                orderrDb.Entry(goods).State = EntityState.Modified;
                orderDb.SaveChanges();
            }
            catch (Exception e)
            {
                string error = e.Message;
                if (e.InnerException != null) error = e.InnerException.Message;
                return BadRequest(error);
            }
            return NoContent();
        }
        [HttpDelete("Order")]
        public ActionResult DeleteOrder(long id)
        {
            try
            {
                var order = orderDb.Orders.FirstOrDefault(t => t.Id == id);
                if (order != null)
                {
                    orderDb.Remove(order);
                    orderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }
        [HttpDelete("Customer")]
        public ActionResult DeleteCustomer(long id)
        {
            try
            {
                var customer = orderDb.Customers.FirstOrDefault(t => t.Id == id);
                if (customer != null)
                {
                    orderDb.Remove(customer);
                    orderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }
        [HttpDelete("Goods")]
        public ActionResult DeleteGoods(long id)
        {
            try
            {
                var order = todoDb.GoodItems.FirstOrDefault(t => t.Id == id);
                if (order != null)
                {
                    orderDb.Remove(order);
                    orderDb.SaveChanges();
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
            return NoContent();
        }











    }
}