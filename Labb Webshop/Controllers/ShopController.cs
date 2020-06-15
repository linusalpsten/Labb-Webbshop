using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Labb_Webshop.Models;
using Labb_Webshop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Labb_Webshop.Controllers
{
    public class ShopController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            var response = await client.GetAsync("https://localhost:44389/Product/GetAll");
            var list = (IEnumerable<Product>)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(IEnumerable<Product>));
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

        public async Task<IActionResult> ViewCart()
        {
            // get list of products
            var response = await client.GetAsync("https://localhost:44389/Product/GetAll");
            var products = (IEnumerable<Product>)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(IEnumerable<Product>));

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

        public async Task<IActionResult> Checkout()
        {
            //payment happens...
            //stock is reduced
            var response = await client.GetAsync("https://localhost:44389/Product/GetAll");
            var products = (IEnumerable<Product>)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(IEnumerable<Product>));

            // count duplicate ids in cartcookie
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
                        client.GetAsync($"https://localhost:44389/Product/ChangeStock/{productId}/{product.Stock - productCount[productId]}");
                    }
                    var json = JsonConvert.SerializeObject(new Order()
                    {
                        UserId = new Guid(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                        ProductId = new Guid(productId),
                        Amount = productCount[productId]
                    });
                    using (var stringContent = new StringContent(json, Encoding.UTF8, "application/json"))
                    {
                        await client.PostAsync("https://localhost:44343/Order/Add", stringContent);
                    }
                }
            }

            //clear cart
            Response.Cookies.Delete("cart");
            return RedirectToAction("Index", "Home");
        }
    }
}