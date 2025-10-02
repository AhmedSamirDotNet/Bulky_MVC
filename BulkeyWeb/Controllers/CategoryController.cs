using Bulky.DataAccess.Data;
using Bulky.DataAccess.Models;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BulkeyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            List<Category> objCtegoryList = _categoryRepository.GetAll().ToList();
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
                _categoryRepository.Add(obj);
                _categoryRepository.Save();
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
            Category? obj = _categoryRepository.Get(c=>c.Id==id);
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
                _categoryRepository.Update(obj);
                _categoryRepository.Save();
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
            Category? obj = _categoryRepository.Get(c=>c.Id==id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost , ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
                Category objToDelete = _categoryRepository.Get(c => c.Id == id);
            if (objToDelete == null)
                {
                    return NotFound();
                }
            _categoryRepository.Remove(objToDelete);
            _categoryRepository.Save();
                TempData["Success"] = "Category Deleted successfully :-)";

            return RedirectToAction("Index", "Category");            
        }

    }
}
