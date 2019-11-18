using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MotoWebShop.Common;
using MotoWebShop.Data;

namespace MotoWebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public ValuesController(ApplicationDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("manufacturers")]
        public ActionResult<IEnumerable<Manufacturer>> GetManufacturers()
        {
            return db.Manufacturers;
        }

        [HttpGet]
        [Route("models")]
        public ActionResult<IEnumerable<Model>> GetModels()
        {
            return db.Models;
        }

        [HttpGet]
        [Route("manufacturers/{id:int}")]
        public ActionResult<IEnumerable<Model>> GetModelsByManufacturer(int id)
        {
            var models = db.Models.Where(x => x.ManufacturerId == id);

            return new JsonResult(models);
        }

        [HttpGet]
        [Route("categories")]
        public ActionResult<IEnumerable<Category>> GetCategories()
        {
            return db.Categories;
        }

        [HttpGet]
        [Route("items")]
        public ActionResult<IEnumerable<Item>> GetItems()
        {
            return db.Items;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public ActionResult<IEnumerable<Item>> GetItemsByCategoryAndModel(int catId, int modelId)
        {
            var items = db.Items.Where(x => x.CategoryId == catId && x.ModelId == modelId);

            return new JsonResult(items);
        }
    }
}