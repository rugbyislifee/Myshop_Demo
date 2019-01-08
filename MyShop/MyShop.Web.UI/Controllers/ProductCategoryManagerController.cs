using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MyShop.Web.UI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        ProductCategoryRepository context;

        public ProductCategoryManagerController()
        {
            context = new ProductCategoryRepository();
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> ProductCategories = context.Collection().ToList();
            return View(ProductCategories);
        }

        public ActionResult Create()
        {
            ProductCategory ProductCategories = new ProductCategory();
            return View(ProductCategories);
        }

        [HttpPost]
        public ActionResult Create(ProductCategory ProductCategories)
        {
            if (ModelState.IsValid)
            {
                context.Insert(ProductCategories);
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
            ProductCategory ProductCategory = context.Find(Id);
            if (ProductCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ProductCategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory product, string Id)
        {
            ProductCategory productCategoryToEdit = context.Find(Id);
            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {

                productCategoryToEdit.Category = product.Category;             
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            ProductCategory producCategoryToDelete = context.Find(Id);
            if (producCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(producCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory producCategoryToDelete = context.Find(Id);
            if (producCategoryToDelete == null)
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
