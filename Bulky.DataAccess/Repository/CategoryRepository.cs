using Bulky.DataAccess.Data;
using Bulky.DataAccess.Models;
using Bulky.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _CategoryRepository;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {//implement this constructor to initialize the _CategoryRepository field and dbSet from child classes
         //when objects are created and pass it to the base class constructor
            _CategoryRepository = db;
        }
        public void Save()
        {
            _CategoryRepository.SaveChanges();
        }

        public void Update(Category category)
        {
         //   _CategoryRepository.Categories.Update(category); //This syntax is correct and i understand
            _CategoryRepository.Update(category); //EF Core automatically detects the type of category and routes
                                                  //it to the correct DbSet behind the scenes.

        }


    }
}
