using eStore.BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;


namespace eStore.DBO
{
    public class ShoppingCartManager
    {
        eStoreDBContext db = new eStoreDBContext();

        public void AddToCart(Product product, string shoppingCartId)
        {
            // Get the matching cart and album instances
            List<Price> prices = db.Price.ToList();
            var cartItem = db.Cart.SingleOrDefault(
                c => c.CartID == shoppingCartId
                && c.ProductId == product.ID);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item existsB             
                cartItem = new Cart
                {

                    ProductId = product.ID,
                    CartID = shoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now,
                    Product = product
                };
                db.Cart.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            db.SaveChanges();
        }

        public int RemoveFromCart(int id, string shoppingCartId)
        {
            db = new eStoreDBContext();

            // Get the cart
            var cartItem = db.Cart.Single(
                cart => cart.CartID == shoppingCartId
                && cart.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    db.Cart.Remove(cartItem);
                }
                // Save changes
                db.SaveChanges();
            }
            return itemCount;
        }

        public void EmptyCart(string shoppingCartId)
        {
            var cartItems = db.Cart.Where(
                cart => cart.CartID == shoppingCartId);

            foreach (var cartItem in cartItems)
            {
                db.Cart.Remove(cartItem);
            }
            // Save changes
            db.SaveChanges();
        }

        public List<Cart> GetCartItems(string shoppingCartId)
        {
            List<Price> prices = db.Price.ToList();
            List<Product> products = db.Product.ToList();
            //List<Cart> cart = db.Cart.ToList();
            return db.Cart.Where(
    cart => cart.CartID == shoppingCartId).ToList();
        }

        public int GetCount(string shoppingCartId)
        {
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in db.Cart
                          where cartItems.CartID == shoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
        }

        public decimal GetTotal(string shoppingCartId)
        {
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in db.Cart
                              where cartItems.CartID == shoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Product.Price.PriceValue).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOrder(Order order, string shoppingCartId)
        {
            decimal orderTotal = 0;

            var cartItems = GetCartItems(shoppingCartId);
            // Iterate over the items in the cart, 
            // adding the order details for each
            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    ProductID = item.ProductId,
                    OrderID = order.ID,
                    Quantity = item.Count
                };
                // Set the order total of the shopping cart
                orderTotal += (item.Count * item.Product.Price.PriceValue);

                db.OrderDetail.Add(orderDetail);

            }
            // Set the order's total to the orderTotal count
            order.Total = orderTotal;

            // Save the order
            db.SaveChanges();
            // Empty the shopping cart
            EmptyCart(shoppingCartId);
            // Return the OrderId as the confirmation number
            return order.ID;
        }

        public void MigrateCart(string userName, string shoppingCartId)
        {
            var shoppingCart = db.Cart.Where(
                c => c.CartID == shoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartID = userName;
            }
            db.SaveChanges();
        }

        public Product GetProductById(int id)
        {
            return db.Product.Single(prod => prod.ID == id);
        }
    }
}
