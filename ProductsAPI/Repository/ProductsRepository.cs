using ProductsAPI.Models;
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

        public Product AddProduct(Product product)
        {
            return new Product();
        }
    }
}
