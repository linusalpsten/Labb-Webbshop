using OrdersAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Repository
{
    public class OrdersRepository
    {
        private readonly OrderContext context = new OrderContext();
        public Order GetById(Guid id)
        {
            return context.Orders.SingleOrDefault(o => o.Id == id);
        }
        public IEnumerable<Order> GetByUserId(Guid userId)
        {
            return context.Orders.Where(o => o.UserId == userId).ToList();
        }
        public IEnumerable<Order> GetByProductId(Guid productId)
        {
            return context.Orders.Where(o => o.ProductId == productId).ToList();
        }
        public IEnumerable<Order> GetAll()
        {
            return context.Orders.ToList();
        }
        public Order Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }
        public Order RemoveById(Guid id)
        {
            var order = GetById(id);
            context.Orders.Remove(order);
            context.SaveChanges();
            return order;
        }
    }
}
