﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoWebShop.Common;
using MotoWebShop.Data;
using MotoWebShop.Models;
using MotoWebShop.ViewModels;

namespace MotoWebShop.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var orderHead = db.OrderHead;
            var orderBody = db.OrderBody;

            ViewData["OrderHead"] = orderHead;
            ViewData["OrderBody"] = orderBody;

            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Manufacturers()
        {
            var manufacturers = db.Manufacturers;

            return View(manufacturers);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddManufacturer(Manufacturer model)
        {
            db.Manufacturers.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Manufacturers));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteManufacturer(int id)
        {
            var model = db.Manufacturers.Where(x => x.Id == id).FirstOrDefault();
            db.Manufacturers.Remove(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Manufacturers));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditManufacturer(int id)
        {
            var model = db.Manufacturers.Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditManufacturer(Manufacturer model)
        {
            var delete = db.Manufacturers.Where(x => x.Id == model.Id).FirstOrDefault();
            db.Remove(delete);
            db.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Manufacturers));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Models()
        {
            var models = db.Models;

            return View(models);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddModel(Model model)
        {
            db.Models.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Models));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteModel(int id)
        {
            var model = db.Models.Where(x => x.Id == id).FirstOrDefault();
            db.Models.Remove(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Models));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditModel(int id)
        {
            var model = db.Models.Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditModel(Model model)
        {
            var delete = db.Models.Where(x => x.Id == model.Id).FirstOrDefault();
            db.Remove(delete);
            db.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Models));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Categories()
        {
            var categories = db.Categories;

            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            db.Categories.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Categories));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteCategory(int id)
        {
            var model = db.Categories.Where(x => x.Id == id).FirstOrDefault();
            db.Categories.Remove(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Categories));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var model = db.Categories.Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditCategory(Category model)
        {
            var delete = db.Categories.Where(x => x.Id == model.Id).FirstOrDefault();
            db.Remove(delete);
            db.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Categories));
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Items()
        {
            var items = db.Items;

            return View(items);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddItem(Item model)
        {
            db.Items.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Items));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteItem(int id)
        {
            var model = db.Items.Where(x => x.Id == id).FirstOrDefault();
            db.Items.Remove(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Items));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult EditItem(int id)
        {
            var model = db.Items.Where(x => x.Id == id).FirstOrDefault();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditItem(Item model)
        {
            var delete = db.Items.Where(x => x.Id == model.Id).FirstOrDefault();
            db.Remove(delete);
            db.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Items));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
