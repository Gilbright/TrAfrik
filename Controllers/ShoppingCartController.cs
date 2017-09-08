using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurAfrikb.Models;
using TurAfrikb.Viewmodels;



namespace TurAfrikb.Controllers
{
    public class ShoppingCartController : Controller
    {


        ShopDb StoreDB = new ShopDb();

        public ActionResult Index()
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);


                var viewModel = new ShoppingCartViewModel
                {
                    CartItems = cart.GetCartProducts(),
                    CartTotal = cart.GetTotal()
                };

                return View(viewModel);
            }

            public ActionResult AddToCart(int id)
            {

                var addedItem = StoreDB.Products
                    .Single(product => product.Id == id);


                var cart = ShoppingCart.GetCart(this.HttpContext);

                cart.AddToCart(addedItem);


                return RedirectToAction("Index");
            }

            [HttpPost]
            public ActionResult RemoveFromCart(int id)
            {

                var cart = ShoppingCart.GetCart(this.HttpContext);


                string itemName = StoreDB.Carts
                    .Single(item => item.RecordId == id).Product.Name;


                int itemCount = cart.RemoveFromCart(id);


                var results = new ShoppingCartRemoveView
                {
                    Message = Server.HtmlEncode(itemName) +
                        " has been removed from your shopping cart.",
                    CartTotal = cart.GetTotal(),
                    CartCount = cart.GetCount(),
                    ItemCount = itemCount,
                    DeleteId = id
                };
                return Json(results);
            }

            [ChildActionOnly]
            public ActionResult CartSummary()
            {
                var cart = ShoppingCart.GetCart(this.HttpContext);

                ViewData["CartCount"] = cart.GetCount();
                return PartialView("CartSummary");
            }
        }
    }


