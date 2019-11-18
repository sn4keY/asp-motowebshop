﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoWebShop.Data;
using MotoWebShop.Models;

namespace MotoWebShop.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
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
        public IActionResult Categories()
        {
            var categories = db.Categories;

            return View(categories);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Items()
        {
            var items = db.Items;

            return View(items);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
