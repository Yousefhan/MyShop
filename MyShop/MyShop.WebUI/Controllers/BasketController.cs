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
        IRepository<Customer> customerContext;
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
            customerContext = new SQLRepository<Customer>(DContext);
        }
        [Authorize]
        public ActionResult CheckOut ()
        {
            Customer customer = customerContext.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    Surename = customer.LastName,
                    ZipCode = customer.ZipCode

                };
                return View(order);

            }
            return RedirectToAction("Error");
        }

        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var basektItem = basketService.GetBasketItems(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;
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