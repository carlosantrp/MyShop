using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        InMemoryRepository<ProductCategory> contex;

        public ProductCategoryManagerController()
        {
            contex = new InMemoryRepository<ProductCategory>();
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductCategory> productCategories = contex.Collection().ToList();
            return View(productCategories);
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
            else
            {
                contex.Insert(productCategory);
                contex.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            ProductCategory productCategory = contex.Find(Id);

            if (productCategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategory);
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory productCategoryToEdit, string Id)
        {
            ProductCategory productToEdit = contex.Find(Id);

            if (productCategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(productCategoryToEdit);
                }
                productToEdit.Category = productCategoryToEdit.Category;

                contex.Commit();
                return RedirectToAction("Index");
            }


        }

        public ActionResult Delete(String Id)
        {
            ProductCategory productCategoryToDelete = contex.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productCategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            ProductCategory productCategoryToDelete = contex.Find(Id);

            if (productCategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                contex.Delete(Id);
                contex.Commit();
                return RedirectToAction("Index");
            }

        }
    }
}