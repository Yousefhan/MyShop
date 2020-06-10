using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.SQL;
using MyShop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IBasketService basketService;
        IRepository<Basket> BasketContext;
        IRepository<Product> ProductContext;
        IOrderService orderService;
        // GET: Basket
        public ActionResult Index()
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            return View(basketItems);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveItem(this.HttpContext, Id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }
        public BasketController()
        {
            DataContext DContext = new DataContext();
            BasketContext = new SQLRepository<Basket>(DContext);
            ProductContext = new SQLRepository<Product>(DContext);
            basketService = new BasketService(ProductContext,BasketContext);
            orderService = new OrderService(new SQLRepository<Order>(DContext));
        }
        public ActionResult CheckOut ()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var basektItem = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            // proccess payment 

            order.OrderStatus = "Payment Proccessed";
            orderService.CreateOrder(order, basektItem);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou",new { OrderId = order.Id });
        }
        public ActionResult ThankYou(string OrderId)
        {
            ViewBag.OrderId = OrderId;
            return View();
        }
    }
}