using eStore.BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eStore.Models
{
    public class ShoppingCartView
    {
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}