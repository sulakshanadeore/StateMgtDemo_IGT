using StateMgtDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StateMgtDemo.Controllers
{
    public class ProductsController : Controller
    {
      public  static List<ProductsModel> products = new List<ProductsModel>()
        {
        new ProductsModel{Prodid=1,ProductName="Iron",Price=2000 },
       new ProductsModel{Prodid=2,ProductName="Phone",Price=20000 },
       new ProductsModel{Prodid=3,ProductName="Laptop",Price=40000 },
       new ProductsModel{ Prodid=4,ProductName="Mouse",Price=1000},
       new ProductsModel{Prodid=5,ProductName="Charger",Price=3000 }
        };

        // GET: Products
        public ActionResult ListOfProducts()
        {
            return View(products);
        }
    }
}