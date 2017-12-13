using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

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

        public ActionResult Index()
        {
            List<Product> products = ProductContex.Collection().ToList();
            return View(products);
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