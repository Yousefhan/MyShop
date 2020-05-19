using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> Context;
        public ProductCategoryManagerController()
        {
            DataContext DContext = new DataContext();
            Context = new SQLRepository<ProductCategory>(DContext);
        }
        // GET: ProductCategoryManager
        public ActionResult Index()
        {
            List<ProductCategory> products = Context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductCategory productCategory = new ProductCategory();
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            Context.Insert(productCategory);
            Context.Commit();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string Id)
        {
            ProductCategory productCategory = Context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory, string Id)
        {
            ProductCategory ProductCategoryToEdit = Context.Find(Id);
            if (ProductCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(productCategory);
            }
            ProductCategoryToEdit.Category = productCategory.Category;
            Context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory productCategory = Context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            return View(productCategory);

        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategory = Context.Find(Id);
            if (productCategory == null)
            {
                return HttpNotFound();
            }
            Context.Delete(Id);
            Context.Commit();
            return RedirectToAction("Index");


        }
    }

}
