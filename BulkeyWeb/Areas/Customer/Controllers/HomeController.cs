using System.Diagnostics;
using Bulky.DataAccess.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkeyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _UOF;


        public HomeController(ILogger<HomeController> logger , IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _UOF = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> objProductList = _UOF.Products.GetAll(includeProperties: "Category");

            return View(objProductList);
        }

        public IActionResult Details(int id)
        {
            Product objProduct = _UOF.Products.Get(p=>(p.Id==id),includeProperties: "Category");

            return View(objProduct);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
