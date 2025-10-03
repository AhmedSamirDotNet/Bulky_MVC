using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyWeb.Areas.Customer.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public ProductController(IUnitOfWork unitOf)
        {
            _UOW = unitOf;
        }

        public IActionResult Index()
        {
            List<Product> objCtegoryList = _UOW.Products.GetAll().ToList();
            return View(objCtegoryList);
        }

        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if(ModelState.IsValid && obj!=null)
            {
                _UOW.Products.Add(obj);
                _UOW.Save();
                TempData["Success"] = "Product created successfully :-)";
                return RedirectToAction("Index", "Product");
            }
            return View("Create", obj);
        }


        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Product? obj = _UOW.Products.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Edit",obj);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if(ModelState.IsValid && obj != null)
            {
                _UOW.Products.Update(obj);
                _UOW.Save();
                TempData["Success"] = "Product updated successfully :-)";
                return RedirectToAction("Index", "Product");
            }
            return View("Edit", obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? obj = _UOW.Products.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Delete", obj);
        }
        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            if(obj == null || obj.Id == 0)
            {
                return NotFound();
            }
            _UOW.Products.Remove(obj);
            _UOW.Save();
            TempData["Success"] = "Product deleted successfully :-)";
            return RedirectToAction("Index", "Product");
        }
    }
}
