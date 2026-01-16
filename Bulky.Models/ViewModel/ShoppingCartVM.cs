using Bulky.Models.Models;


namespace Bulky.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart>? ShoppingCartList { get; set; }
        public OrderHeader? OrderHeader { get; set; }
        //public double OrderTotal { get; set; }
    }
}
