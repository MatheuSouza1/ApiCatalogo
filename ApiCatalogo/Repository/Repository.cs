using ApiCatalogo.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalogo.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ApiCatalogoContext _context;
        public Repository(ApiCatalogoContext Context) 
        {
            _context = Context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
    }
}
