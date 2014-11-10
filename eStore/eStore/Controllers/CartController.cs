using eStore.BL.BO;
using eStore.DBO;
using eStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eStore.Controllers
{
    public class CartController : Controller
    {
        #region Private Members

        public string ShoppingCartId { get; set; }
        ShoppingCartManager dbo = new ShoppingCartManager();
        public new HttpContextBase HttpContext
        {
            get
            {
                HttpContextWrapper context =
                    new HttpContextWrapper(System.Web.HttpContext.Current);
                return (HttpContextBase)context;
            }
        }

        #endregion
        //
        // GET: /Checkout/
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Set up our ViewModel
            var viewModel = new ShoppingCartView
            {
                CartItems = dbo.GetCartItems(ShoppingCartId),
                CartTotal = dbo.GetTotal(ShoppingCartId)
            };

            // Return the view
            return View(viewModel);
        }


        // GET: /Store/AddToCart/5
        public ActionResult AddToCart(int id)
        {
            // Retrieve the album from the database
            Product addedProduct = dbo.GetProductById(id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            dbo.AddToCart(addedProduct, ShoppingCartId);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        //
        // AJAX: /ShoppingCart/RemoveFromCart/5
        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            // Remove the item from the cart
            var cart = ShoppingCart.GetCart(this.HttpContext);

            // Get the name of the album to display confirmation
            string productName = dbo.GetProductById(id).Name;

            // Remove from cart
            int itemCount = dbo.RemoveFromCart(id, ShoppingCartId);

            return RedirectToAction("Index");
        }
        //
        // GET: /ShoppingCart/CartSummary
        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);
            return View();

            //ViewData["CartCount"] = dbo.GetCount(ShoppingCartId);
            //return PartialView("CartSummary");
        }
	}
}