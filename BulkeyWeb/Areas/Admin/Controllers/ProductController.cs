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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOf, IWebHostEnvironment webHostEnvironment)
        {
            _UOW = unitOf;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objCtegoryList = _UOW.Products.GetAll(includeProperties: "Category").ToList();

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
            if (id == null || id == 0)
            {
                //create
                return View("Upsert", productVM);
            }
            else
            {
                //update
                productVM.Product = _UOW.Products.Get(c => c.Id == id);
                if (productVM.Product == null)
                {
                    return NotFound();
                }
                return View("Upsert", productVM);
            }
        }
        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if (ModelState.IsValid && obj != null)
            {

                if (file != null)
                {
                    string ProductFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "product");
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string filePath = Path.Combine(ProductFolderPath, fileName);



                    //In case of update
                    if (!string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        //Update image   > so delete the old one
                        //next line doesn't work why??????
                        string OldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.Product.ImageUrl.TrimStart('/'));// علشان الريلاتق باث متخزن في الاول بسلاش فترم علشان الكومباين بتحط واحده
                        if (System.IO.File.Exists(OldImagePath))
                        {
                            //Be sure It exists then delete it
                            System.IO.File.Delete(OldImagePath);
                        }

                    }

                    using (var filestream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    obj.Product.ImageUrl = @"/images/product/" + fileName;

                }

                if (obj.Product.Id == 0)
                {
                    _UOW.Products.Add(obj.Product);
                }
                else
                {
                    _UOW.Products.Update(obj.Product);
                }
                _UOW.Save();
                TempData["Success"] = "Product created successfully :-)";
                return RedirectToAction("Index", "Product");
            }

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryListItem = _UOW.Categories.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };
            return View("Upsert", obj);
        }


        //public IActionResult Edit(int? id)
        //{
        //    if(id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? obj = _UOW.Products.Get(c => c.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    return View("Edit",obj);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    if(ModelState.IsValid && obj != null)
        //    {
        //        _UOW.Products.Update(obj);
        //        _UOW.Save();
        //        TempData["Success"] = "Product updated successfully :-)";
        //        return RedirectToAction("Index", "Product");
        //    }
        //    return View("Edit", obj);
        //}

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
        //[HttpPost]
        //public IActionResult Delete(Product obj)
        //{
        //    if(obj == null || obj.Id == 0)
        //    {
        //        return NotFound();
        //    }
        //    _UOW.Products.Remove(obj);
        //    _UOW.Save();
        //    TempData["Success"] = "Product deleted successfully :-)";
        //    return RedirectToAction("Index", "Product");
        //}
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var productList = _UOW.Products.GetAll(includeProperties: "Category");
            return Json(new { data = productList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var product = _UOW.Products.Get(p => p.Id == id);

            if (product == null)
                return Json(new { success = false, message = "Error while deleteing" });

            string OldIamgePath = Path.Combine(_webHostEnvironment.WebRootPath, product.ImageUrl.TrimStart('/', '\\'));
            if (System.IO.File.Exists(OldIamgePath))
                System.IO.File.Delete(OldIamgePath);

            _UOW.Products.Remove(product);
            _UOW.Save();
            return Json(new { success = true, message = "product deleted successfully" });
        }
        #endregion
    }

}