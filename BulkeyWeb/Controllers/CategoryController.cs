using BulkeyWeb.Data;
using BulkeyWeb.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
