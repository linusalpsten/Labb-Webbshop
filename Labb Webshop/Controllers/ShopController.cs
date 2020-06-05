using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labb_Webshop.Models;
using Labb_Webshop.ViewModels;
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
                    Stock = 0
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                },

            };
            return View(list);
        }

        public IActionResult AddToCart(Guid Id, string redirect = "Index")
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            string cartContent = "";

            if (!string.IsNullOrEmpty(cart.Value))
            {
                cartContent = cart.Value;
                cartContent += "," + Id;
            }
            else
            {
                cartContent += Id;
            }

            Response.Cookies.Append("cart", cartContent);
            return RedirectToAction(redirect);
        }

        public IActionResult RemoveFromCart(Guid Id)
        {
            var cart = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            if (!string.IsNullOrEmpty(cart.Value))
            {
                var productIds = cart.Value.Split(",").ToList();
                productIds.Remove(Id.ToString());
                string cartContent = string.Join(",", productIds);
                Response.Cookies.Append("cart", cartContent);
            }
            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            // get list of products
            var products = new List<Product>()
            {
                new Product
                {
                    Name = "TestItem",
                    Description = "Item for testing",
                    Price = 10m,
                    ImagePath = "/default.png",
                    Stock = 10
                }
            };

            // count duplicate ids in cartcookie and add corresponding products to cart with count
            var cart = new List<CartProductViewModel>();
            var cartCookie = Request.Cookies.SingleOrDefault(c => c.Key == "cart");
            if (!string.IsNullOrEmpty(cartCookie.Value))
            {
                Dictionary<string, int> productCount = new Dictionary<string, int>();
                var cartContent = cartCookie.Value.Split(",");
                foreach (string productId in cartContent)
                {
                    int count = 0;
                    productCount.TryGetValue(productId, out count);
                    productCount[productId] = count + 1;
                }

                foreach (string productId in productCount.Keys)
                {
                    var product = products.SingleOrDefault(p => p.Id == new Guid(productId));
                    if (product != null)
                    {
                        cart.Add(new CartProductViewModel { Product = product, Count = productCount[productId] });
                    }
                }
            }
            return View(cart);
        }

        public IActionResult Checkout()
        {
            //payment happens...
            //stock is reduced

            //clear cart
            Response.Cookies.Delete("cart");
            return RedirectToAction("Index", "Home");
        }
    }
}