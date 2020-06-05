using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int Stock { get; set; }

        public static bool operator ==(Product p1, Product p2)
        {
            bool equal = true;
            if (p1.Id != p2.Id)
            {
                equal = false;
            }
            if (p1.Name != p2.Name)
            {
                equal = false;
            }
            if (p1.Description != p2.Description)
            {
                equal = false;
            }
            if (p1.Price != p2.Price)
            {
                equal = false;
            }
            if (p1.ImagePath != p2.ImagePath)
            {
                equal = false;
            }
            if (p1.Stock != p2.Stock)
            {
                equal = false;
            }
            return equal;
        }

        public static bool operator !=(Product p1, Product p2)
        {
            return !(p1 == p2);
        }
    }
}
