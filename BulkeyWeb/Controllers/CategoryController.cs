using Bulky.DataAccess.Data;
using Bulky.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkeyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCtegoryList = _db.Categories.ToList();
            return View(objCtegoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToString() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name");
            }


            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category created successfully :-)";
                return RedirectToAction("Index", "Category");
            }
            return View("Create", obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }   
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name.ToString() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order cannot exactly match the name");
            }


            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated successfully :-)";

                return RedirectToAction("Index", "Category");
            }
            return View("Edit", obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _db.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
                Category objToDelete = _db.Categories.Find(id);
                if(objToDelete == null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(objToDelete);
                _db.SaveChanges();
                TempData["Success"] = "Category Deleted successfully :-)";

            return RedirectToAction("Index", "Category");            
        }

    }
}
