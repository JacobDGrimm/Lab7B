﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LanguageFeatures.Models;
using System.Text;

namespace LanguageFeatures.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public string Index()
        {
            return "Navagate to a URL to show an example";
        }

        public ViewResult AutoProperty()
        {
            // create a new Product object
            Product myProduct = new Product();

            // set the property value
            myProduct.Name = "Kayak";

            // get the property
            string productName = myProduct.Name;

            // generate the view
            return View("Result", (object)String.Format("Product name: {0}", productName));
        }

        public ViewResult CreateProduct()
        {
            // create and populate a new Product object
            Product myProduct = new Product
            {
                ProductID = 100,
                Name = "Kayak",
                Description = "A boat for one person",
                Price = 275M,
                Category = "Watersports"
            };
            return View("Result", (object)String.Format("Category: {0}", myProduct.Category));
        }

        public ViewResult CreateCollection()
        {
            string[] stringArray = { "apple", "orange", "plum" };
            List<int> intList = new List<int> { 10, 20, 30, 40 };
            Dictionary<string, int> myDict = new Dictionary<string, int>
            {
                { "apple", 10 }, { "orange", 20 }, {"plum", 30 }
            };
            return View("Result", (object)stringArray[1]); 
        }

        public ViewResult UseExtension()
        {
            //create and populate ShoppingCart
            ShoppingCart cart = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "Kayak", Price = 275M },
                    new Product { Name = "LifeJacket", Price = 48.95M },
                    new Product { Name = "Soccer ball", Price = 19.50M },
                    new Product { Name = "Corner flag", Price = 34.95M }
                }
            };
            //get the total value of the products in the cart
            decimal cartTotal = cart.TotalPrices();
            return View("Result", (object)String.Format("Total: {0:c}", cartTotal));
        }

        public ViewResult UseExtensionEnumerable()
        {
            IEnumerable<Product> products = new ShoppingCart
            { 
                Products = new List<Product>
                {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball" , Price = 19.50M },
                new Product { Name = "Corner flag" , Price = 34.95M }
                }
            };
            //create and populate an array of Product objects
            Product[] productArray =
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Corner flag", Price = 34.95M }
            };
            //get the total value of the products in the cart
            decimal cartTotal = products.TotalPrices();
            decimal arrayTotal = products.TotalPrices();
            return View("Result", (object)String.Format("Cart Total: {0}, Array Total: {1}", cartTotal, arrayTotal));
        }

        public ViewResult UseFilterExtensionMethod()
        {
            IEnumerable<Product> products = new ShoppingCart
            {
                Products = new List<Product>
                {
                    new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
                    new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                    new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
                    new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M }
                }
            };
            
            Func<Product, bool> categoryFilter = prod => prod.Category == "Soccer";

            decimal total = 0;

            foreach (Product prod in products.Filter(prod => prod.Category == "Soccer" || prod.Price > 20))
            {
                total += prod.Price;
            }
            return View("Result", (object)String.Format("Total: {0}", total));
        }

        public ViewResult CreateAnonArray()
        {
            var oddsAndEnds = new[]
            {
                new { Name = "MVC", Category = "Pattern" },
                new { Name = "Hat", Category = "Clothing" },
                new { Name = "Apple", Category = "Fruit" }
            };
            StringBuilder result = new StringBuilder();
            foreach (var item in oddsAndEnds)
            {
                result.Append(item.Name).Append(" ");
            }
            return View("Result", (object)result.ToString());
        }

        public ViewResult FindProducts()
        {
            Product[] products =
            {
                new Product { Name = "Kayak", Category = "Watersports", Price = 275M },
                new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95M },
                new Product { Name = "Soccer ball", Category = "Soccer", Price = 19.50M },
                new Product { Name = "Corner flag", Category = "Soccer", Price = 34.95M }
            };
            /*SQL LINQ statement
            var foundProducts = from match in products
                                orderby match.Price descending
                                select new { match.Name, match.Price };
            DOT NOTATION LINQ STATEMENT */
            var foundProducts =
                products.OrderByDescending( e => e.Price).Take(3).Select( e => new { e.Name, e.Price });
            // create the results
            int count = 0;
            StringBuilder result = new StringBuilder();
            foreach (var p in foundProducts)
            {
                result.AppendFormat("Price: {0} ", p.Price);
                if (++count == 3)
                {
                    break;
                }
            }
            return View("Result", (object)result.ToString());
        }
    }
}