using ProductsAPI.Models;
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
            var id = Guid.Empty;
            var productsRepository = new ProductsRepository();
            var product = productsRepository.GetById(id);

            Assert.IsType<Product>(product);
            Assert.Equal(id, product.Id);
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
            var id = Guid.Empty;
            var productsRepository = new ProductsRepository();

            //change and test twice with different values to make sure the stock actually changed
            productsRepository.ChangeStock(id, 5);
            var product = productsRepository.GetById(id);
            Assert.Equal(5, product.Stock);

            productsRepository.ChangeStock(id, 4);
            product = productsRepository.GetById(id);
            Assert.Equal(4, product.Stock);
        }
    }
}
