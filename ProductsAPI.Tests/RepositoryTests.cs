﻿using ProductsAPI.Models;
using ProductsAPI.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xunit;

namespace ProductsAPI.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void GetProductById_Returns_Product()
        {
            var productsRepository = new ProductsRepository();
            var added = productsRepository.Add(new Product
            {
                Name = "xUnit",
                Description = "xUnit",
                Price = 10m,
                ImagePath = "/default.png",
                Stock = 10
            });
            var product = productsRepository.GetById(added.Id);

            Assert.IsType<Product>(product);
            Assert.Equal(added.Id, product.Id);
        }

        [Fact]
        public void GetAll_Returns_Products()
        {
            var productsRepository = new ProductsRepository();

            var products = productsRepository.GetAll();
            var productIds = new List<Guid>();

            bool uniqueIds = true;
            foreach (var product in products)
            {
                if (productIds.Contains(product.Id))
                {
                    uniqueIds = false;
                    break;
                }
                productIds.Add(product.Id);
            }
            Assert.True(uniqueIds);
        }

        [Fact]
        public void ChangeStock_Changes_ProductStock()
        {
            var productsRepository = new ProductsRepository();
            var added = productsRepository.Add(new Product
            {
                Name = "xUnit",
                Description = "xUnit",
                Price = 10m,
                ImagePath = "/default.png",
                Stock = 10
            });

            //change and test twice with different values to make sure the stock actually changed
            productsRepository.ChangeStock(added.Id, 5);
            var product = productsRepository.GetById(added.Id);
            Assert.Equal(5, product.Stock);

            productsRepository.ChangeStock(added.Id, 4);
            product = productsRepository.GetById(added.Id);
            Assert.Equal(4, product.Stock);
        }

        [Fact]
        public void AddProduct_Adds_Product()
        {
            var productsRepository = new ProductsRepository();
            var product = productsRepository.Add(new Product
            {
                Name = "xUnit",
                Description = "xUnit",
                Price = 10m,
                ImagePath = "/default.png",
                Stock = 10
            });

            //assumes GetById works
            var dbProduct = productsRepository.GetById(product.Id);
            Assert.Equal(product, dbProduct);
        }

        [Fact]
        public void RemoveById_Removes_Product()
        {
            var productsRepository = new ProductsRepository();
            // add a product to remove
            var product = productsRepository.Add(new Product
            {
                Name = "xUnit",
                Description = "xUnit",
                Price = 10m,
                ImagePath = "/default.png",
                Stock = 10
            });

            productsRepository.RemoveById(product.Id);
            // assumes GetAll works
            var products = productsRepository.GetAll();
            Assert.DoesNotContain(products, p => p.Id == product.Id);
        }
    }
}
