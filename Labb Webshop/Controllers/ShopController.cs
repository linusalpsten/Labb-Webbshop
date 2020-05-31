using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb_Webshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labb_Webshop.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            var list = new List<Product>()
            {
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    NrInStock = 10
                },

            };
            return View(list);
        }

        public IActionResult AddToCard(Guid Id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = "";

            if (cart.Value != null)
            {
                cartContent = cart.Value;
                cartContent += "," + Id;
            }
            else
            {
                cartContent += Id;
            }

            Response.Cookies.Append("cart", cartContent);

            return RedirectToAction("Index");
        }
    }
}