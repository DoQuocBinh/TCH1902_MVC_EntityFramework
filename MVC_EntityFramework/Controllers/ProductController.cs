using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_EntityFramework.Models;

namespace MVC_EntityFramework.Controllers
{
    public class ProductController : Controller
    {
        MyPetShopContext db = new MyPetShopContext();
        public IActionResult Index()
        {
            //db.Categories.ToList();
            //load all products with it's categories
            return View(db.Products.Include(c=>c.CategoryNavigation).ToList());
        }
        public IActionResult Create()
        {
            //ViewBag is to share data between Controller and View
            var categories = db.Categories.ToList();
            ViewBag.category = categories;
            return View();
        }
        public async Task<IActionResult>  DoCreate(IFormFile postedFile, Product product)
        {
            var invalid = false;
            if(product.ProductName.Length < 3)
            {
                ModelState.AddModelError("ProductName", "Name length must >= 3 characters");
                invalid = true;
            }
            if (invalid)
            {
                var categories = db.Categories.ToList();
                ViewBag.category = categories;
                return View("Create");
            }
            using (var dataStream = new MemoryStream())
            {
                await postedFile.CopyToAsync(dataStream);
                product.Picture = dataStream.ToArray();
            }
            db.Products.Add(product);
            db.SaveChanges();
            return View("Index", db.Products.Include(p => p.CategoryNavigation).ToList());
        }

    }
}
