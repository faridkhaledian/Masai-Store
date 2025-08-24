using _0_Framework.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace _0_Framework.Infrastructure
{
    public class RepositoryBase<TKey, T> : IRepository<TKey, T> where T : class
    {
        private readonly DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }
        #region GetById
        public T Get(TKey id)
        {
            return _context.Find<T>(id);
        }
        #endregion

        #region GetAll
        public List<T> Get()
        {
            return _context.Set<T>().ToList();
        }
        #endregion

        #region Create
        public void Create(T entity)
        {
            _context.Add(entity);
        }
        #endregion

        #region Exists
        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Any(expression);
        }

        #endregion

        #region SaveChange
        public void SaveChange()
        {
            _context.SaveChanges();
        }
        #endregion

        #region Delete
        public void Delete(TKey id)
        {
            var entity = _context.Find<T>(id);
            _context.Remove(entity);
        }
        #endregion


    }
}
