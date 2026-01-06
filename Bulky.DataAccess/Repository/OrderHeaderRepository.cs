using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _OrderHeaderRepository;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _OrderHeaderRepository = db;
        }
       

        public void Update(OrderHeader orderHeader)
        {
            _OrderHeaderRepository.Update(orderHeader);
        }


    }
}
