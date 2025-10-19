using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; } // Add this line if you have a ProductRepository

        public ICompanyRepository Companies { get;private set; }

        public IShoppingCartRepository ShoppingCarts { get; private set; }
        public IApplicationUserRepository ApplicationUsers { get; set; }

        public UnitOfWork (ApplicationDbContext db)
        {
            _db = db;
            Categories = new CategoryRepository(_db);
            Products = new ProductRepository(_db); // Initialize the ProductRepository
            Companies = new CompanyRepository(_db);
            ShoppingCarts = new ShoppingCartRepository(_db);
            ApplicationUsers = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
