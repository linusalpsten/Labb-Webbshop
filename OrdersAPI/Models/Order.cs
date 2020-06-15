using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }

        public static bool operator ==(Order o1, Order o2)
        {
            if (o1.Id != o2.Id)
            {
                return false;
            }
            if (o1.UserId != o2.UserId)
            {
                return false;
            }
            if (o1.ProductId != o2.ProductId)
            {
                return false;
            }
            if (o1.Amount != o2.Amount)
            {
                return false;
            }
            return true;
        }

        public static bool operator !=(Order o1, Order o2)
        {
            return !(o1 == o2);
        }
    }
}
