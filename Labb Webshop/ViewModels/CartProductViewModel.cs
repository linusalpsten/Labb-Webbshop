using Labb_Webshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labb_Webshop.ViewModels
{
    public class CartProductViewModel
    {
        public Product Product { get; set; }
        public int Count { get; set; }
    }
}
