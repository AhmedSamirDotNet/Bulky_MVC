using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db) //implement this constructor to initialize the _CategoryRepository field and dbSet from child classes when objects are created
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> Filter)
        {
            IQueryable<T> query = dbSet; //Defines a query related to this table
            query = query.Where(Filter); // Applies the filter to the query it's like a sql where clause because is compiled filter that will execute on the database server side
            return query.FirstOrDefault(); //Executes the query and returns the first result or null
        }

        public IEnumerable<T> GetAll()
        {
            //_CategoryRepository.Set<T>().Where(); is it possible to filter here ?
            IQueryable<T> query = dbSet; //Defines a query related to this table
            return query.ToList(); //Executes the query and returns the list of results
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}
