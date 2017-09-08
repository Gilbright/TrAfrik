using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TurAfrikb.Models
{
    public class ShoppingCart
    {
        ShopDb storeDb = new ShopDb();
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        //Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        public void AddToCart(Product product)
        {
            var cartProduct = storeDb.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
               && c.ProductId == product.Id);

            if (cartProduct == null)
            {
                cartProduct = new Cart
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDb.Carts.Add(cartProduct);
            }
            else
            {
                cartProduct.Count++;
            }
            storeDb.SaveChanges();
        }


        public int RemoveFromCart(int id)
        {
            var cartProduct = storeDb.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id
                );
            int productCount = 0;
            if (cartProduct != null)
            {
                if (cartProduct.Count > 1)
                {
                    cartProduct.Count--;
                    productCount = cartProduct.Count;   
                }
                else
                {
                    storeDb.Carts.Remove(cartProduct);
                }
                storeDb.SaveChanges();

            }

            return productCount;

        }

        public void EmptyCart()
        {
            var cartProducts = storeDb.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartProduct in cartProducts)
            {
                storeDb.Carts.Remove(cartProduct);
            }

            storeDb.SaveChanges();
        }
        public List<Cart> GetCartProducts()
        {
            return storeDb.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();   
                
        }
        public int GetCount()
        {
            int? count = (from cartProducts in storeDb.Carts
                          where cartProducts.CartId == ShoppingCartId
                          select (int?)cartProducts.Count).Sum();

            return count ?? 0;   
        }

        public decimal GetTotal()
        {
            decimal? total = (from cartProducts in storeDb.Carts
                              where cartProducts.CartId == ShoppingCartId
                              select (int?)cartProducts.Count *
                              cartProducts.Product.Price).Sum();

            return total ?? decimal.Zero;
        }

        public int CreateOder(Order Order)
        {
            decimal orderTotal = 0;
            var cartProducts = GetCartProducts();
            foreach (var item in cartProducts)
            {
                var orderDetail = new OrderDetail
                {
                    ProductId = item.ProductId,
                    OrderId = Order.OrderId,
                    UnitPrice = item.Product.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Product.Price);

                storeDb.OrderDetails.Add(orderDetail);

            }

            Order.Total = orderTotal;

            storeDb.SaveChanges();

            EmptyCart();
            return Order.OrderId;
        }

        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] = context.User.Identity.Name;
                }
                else
                {
                    Guid tenpCartId = Guid.NewGuid();
                    context.Session[CartSessionKey] = tenpCartId.ToString();

                }
            }
            return context.Session[CartSessionKey].ToString();
        }

        public void MigrateCart(string userName)
        {
            var shoppingCart = storeDb.Carts.Where(
                c => c.CartId == ShoppingCartId);
            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDb.SaveChanges();
        }


































    }

}






