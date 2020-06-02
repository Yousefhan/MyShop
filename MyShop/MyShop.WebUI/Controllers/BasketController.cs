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
        }
    }
}