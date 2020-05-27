using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> productCategories;
        public HomeController()
        {
            DataContext DContext = new DataContext();
            Context = new SQLRepository<Product>(DContext);
            productCategories = new SQLRepository<ProductCategory>(DContext);
        }
        public ActionResult Index(string Category=null)
        {
            List<Product> products;
            List<ProductCategory> Categories = productCategories.Collection().ToList();
            if (Category == null)
            {
                products = Context.Collection().ToList();
            }
            else
            {
                products = Context.Collection().Where(p => p.Category == Category).ToList();

            }
            ProductListViewModel model = new ProductListViewModel();
            model.product = products;
            model.productCategories = Categories;
            return View(model);
        }
        public ActionResult Details(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}