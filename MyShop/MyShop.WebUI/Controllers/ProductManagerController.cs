﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using MyShop.DataAccess.SQL;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        IRepository<Product> Context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController()
        {
            DataContext DContext = new DataContext();
            Context = new SQLRepository<Product>(DContext);
            productCategories = new SQLRepository<ProductCategory>(DContext);
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
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
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
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
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
            if (file != null)
            {
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
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