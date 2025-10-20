using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Bulky.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BulkeyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _Uof;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork Uof)
        {
            _Uof = Uof;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new ShoppingCartVM()
            {
                ShoppingCartList = _Uof.ShoppingCarts.GetAll(cart => cart.ApplicationUserId == userId
                , includeProperties: "Product")
            };
            foreach (var shoppingCart in ShoppingCartVM.ShoppingCartList)
            {
                shoppingCart.Price = GetPriceBasedOnQuantity(shoppingCart);
                ShoppingCartVM.OrderTotal += (shoppingCart.Price * shoppingCart.Count);
            }
            return View(ShoppingCartVM);
        }

        public IActionResult Plus(int CartId)
        {
            var CartFromDb = _Uof.ShoppingCarts.Get(cart => cart.Id == CartId);
            CartFromDb.Count += 1;
            _Uof.ShoppingCarts.Update(CartFromDb);
            _Uof.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int CartId)
        {
            var CartFromDb = _Uof.ShoppingCarts.Get(cart => cart.Id == CartId);
            if (CartFromDb.Count <= 1)
            {
                _Uof.ShoppingCarts.Remove(CartFromDb);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                CartFromDb.Count -= 1;
                _Uof.ShoppingCarts.Update(CartFromDb);
            }
            _Uof.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int CartId)
        {
            var CartFromDb = _Uof.ShoppingCarts.Get(cart => cart.Id == CartId);
            _Uof.ShoppingCarts.Remove(CartFromDb);
            _Uof.Save();
            return RedirectToAction(nameof(Index));
        }

        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.Count < 50)
            {

                return shoppingCart.Product.Price;
            }
            else if (shoppingCart.Count < 100)
            {
                return shoppingCart.Product.Price50;
            }
            else
            {
                return shoppingCart.Product.Price100;
            }


        }



    }
}
