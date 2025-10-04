using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.ViewModels;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public IActionResult Upsert(int? id)
        {
            
          //  ViewBag.CategoryList = listItems;
          ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryListItem = _UOW.Categories.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
          };
            if(id==null || id==0)
            {
                //create
                return View("Upsert", productVM);
            }
            else
            {
                //update
                productVM.Product = _UOW.Products.Get(c => c.Id == id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View("Upsert", productVM);
            }
        }
        [HttpPost]
        public IActionResult Create(ProductVM obj)
        {
            if(ModelState.IsValid && obj!=null)
            {
                _UOW.Products.Add(obj.Product);
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
