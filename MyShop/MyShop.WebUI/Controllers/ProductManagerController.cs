using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        IRepository<Product> contex;
        IRepository<ProductCategory> ProductCategories;

        public ProductManagerController(IRepository<Product> ProductContex, IRepository<ProductCategory> ProductCategoriesContex)
        {

            this.contex = ProductContex;
            this.ProductCategories = ProductCategoriesContex;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = contex.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {

            ProductManagerViewModel viewModel = new ProductManagerViewModel();

            viewModel.Product = new Product();

            viewModel.ProductCategories = ProductCategories.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);

                }

                contex.Insert(product);
                contex.Commit();
                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(String Id)
        {
            Product product = contex.Find(Id);

            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = ProductCategories.Collection();
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            Product productToEdit = contex.Find(product.Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                contex.Commit();
                return RedirectToAction("Index");
            }


        }

        public ActionResult Delete(String Id)
        {
            Product productToDelete = contex.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = contex.Find(Id);

            if (productToDelete == null)
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