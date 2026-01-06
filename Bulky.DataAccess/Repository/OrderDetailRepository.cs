using Bulky.DataAccess.Data;
using Bulky.DataAccess.Models;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail> , IOrderDetailRepository
    {
        private readonly ApplicationDbContext _OrderDetailRepository;
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _OrderDetailRepository = db;
        }
       

        public void Update(OrderDetail order)
        {
            _OrderDetailRepository.Update(order); 

        }


    }
}
