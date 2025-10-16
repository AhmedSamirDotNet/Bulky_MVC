using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAccess.ViewModels;
using Bulky.Models.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkeyWeb.Areas.Customer.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]

    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _UOW;
        public CompanyController(IUnitOfWork unitOf)
        {
            _UOW = unitOf;
        }

        public IActionResult Index()
        {
            List<Company> objComapnyList = _UOW.Companies.GetAll().ToList();

            return View(objComapnyList);
        }

        public IActionResult Upsert(int? id)
        {
                      
            if (id == null || id == 0) 
            {
                //create
                return View("Upsert", new Company());
            }
            else
            {
                //update
                Company CompanyObj = _UOW.Companies.Get(c => c.ID == id);
                if (CompanyObj == null)
                {
                    return NotFound();
                }
                return View("Upsert", CompanyObj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid && CompanyObj != null)
            {

                if (CompanyObj.ID == 0)
                {
                    _UOW.Companies.Add(CompanyObj);
                }
                else
                {
                    _UOW.Companies.Update(CompanyObj);
                }
                _UOW.Save();
                TempData["Success"] = "Company created successfully :-)";
                return RedirectToAction("Index", "Company");
            }

            
            return View("Upsert", CompanyObj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? obj = _UOW.Companies.Get(c => c.ID == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View("Delete", obj);
        }
       
        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            var CompanyList = _UOW.Companies.GetAll();
            return Json(new { data = CompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var Company = _UOW.Companies.Get(p => p.ID == id);

            if (Company == null)
                return Json(new { success = false, message = "Error while deleteing" });

            

            _UOW.Companies.Remove(Company);
            _UOW.Save();
            return Json(new { success = true, message = "Company deleted successfully" });
        }
        #endregion
    }

}