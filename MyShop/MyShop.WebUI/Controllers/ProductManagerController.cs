using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository Context;
        ProductCategoryRepository productCategories;
        public ProductManagerController()
        {
            Context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = new Product();
            viewModel.productCategories = productCategories.Collection();
            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            Context.Insert(product);
            Context.Commit();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.product = product;
            viewModel.productCategories = productCategories.Collection();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product ProductToEdit = Context.Find(Id);
            if (ProductToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            ProductToEdit.Category = product.Category;
            ProductToEdit.Description = product.Description;
            ProductToEdit.Image = product.Image;
            ProductToEdit.Name = product.Name;
            ProductToEdit.Price = product.Price;
            Context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            Context.Delete(Id);
            Context.Commit();
            return RedirectToAction("Index");


        }
    }
}