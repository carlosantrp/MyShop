using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {

        IRepository<Product> ProductContex;
        IRepository<ProductCategory> ProductCategoryContex;

        public HomeController(IRepository<Product> ProductContex, IRepository<ProductCategory> ProductCategoryContex)
        {
            this.ProductContex = ProductContex;
            this.ProductCategoryContex = ProductCategoryContex;
        }

        public ActionResult Index(string Category = null)
        {

            List<Product> Products;
            List<ProductCategory> ProductsCategories = ProductCategoryContex.Collection().ToList();

            if (Category == null)
            {
                Products = ProductContex.Collection().ToList();
            }
            else
            {
                Products = ProductContex.Collection().Where(x => x.Category == Category).ToList();
            }

            ProductListViewModel model = new ProductListViewModel();

            model.Products = Products;
            model.ProductCategories = ProductsCategories;

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = ProductContex.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }
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