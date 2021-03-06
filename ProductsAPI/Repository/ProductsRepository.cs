﻿using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Repository
{
    public class ProductsRepository
    {
        private readonly ProductContext context = new ProductContext();
        public Product GetById(Guid id)
        {
            return context.Products.SingleOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }

        public void ChangeStock(Guid id, int newStock)
        {
            var product = GetById(id);
            product.Stock = newStock;
            context.SaveChanges();
        }

        public Product Add(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product RemoveById(Guid id)
        {
            var product = GetById(id);
            context.Products.Remove(product);
            context.SaveChanges();
            return product;
        }
    }
}
