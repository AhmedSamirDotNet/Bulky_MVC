using Bulky.DataAccess.Data;
using Bulky.DataAccess.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkeyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public CategoryController(IUnitOfWork UOW)
        {
            _UOW = UOW;
        }
        public IActionResult Index()
        {
            List<Category> objCtegoryList = _UOW.Categories.GetAll().ToList();
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
                _UOW.Categories.Add(obj);
                _UOW.Save();
                TempData["Success"] = "Categories created successfully :-)";
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
            Category? obj = _UOW.Categories.Get(c=>c.Id==id);
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
                _UOW.Categories.Update(obj);
                _UOW.Save();
                TempData["Success"] = "Categories Updated successfully :-)";

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
            Category? obj = _UOW.Categories.Get(c=>c.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
                Category objToDelete = _UOW.Categories.Get(c => c.Id == id);
            if (objToDelete == null)
                {
                    return NotFound();
                }
            _UOW.Categories.Remove(objToDelete);
                _UOW.Save();
                TempData["Success"] = "Categories Deleted successfully :-)";

            return RedirectToAction("Index", "Category");            
        }

    }
}
