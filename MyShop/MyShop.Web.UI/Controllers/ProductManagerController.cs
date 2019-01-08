using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;

namespace MyShop.Web.UI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepositorycs context;
        ProductCategoryRepository productCategories;

        public ProductManagerController()
        {
            context = new ProductRepositorycs();
            productCategories = new ProductCategoryRepository();
        }
        
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
            else
            {
               
                return View("Create");
            }
        }

        public ActionResult Edit(String Id)
        {
            /* 'aca estamos asignando ID ingresado de persona a productos, despues estamos validando
              si existe o no' */
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = context.Find(Id);
            if(productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
               
                productToEdit.Category = product.Category;
                productToEdit.Descritption = product.Descritption;
                productToEdit.Iamge = product.Iamge;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product producToDelete = context.Find(Id);
            if (producToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(producToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product producToDelete = context.Find(Id);
            if (producToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }

    }
}