﻿using ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAPI.Repository
{
    public class ProductsRepository
    {
        public Product GetById(Guid id)
        {
            return new Product();
        }

        public IEnumerable<Product> GetAll()
        {
            return new List<Product>();
        }

        public void ChangeStock(Guid id, int newStock)
        {

        }
    }
}