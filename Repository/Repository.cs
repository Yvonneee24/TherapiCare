using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using TherapiCareTest.Data;
using TherapiCareTest.Repository.IRepository;

namespace TherapiCareTest.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
            _db.Schedules.Include(u => u.ProgramStudent).Include(u => u.ProgramStudentId).Include(u => u.Slot).Include(u => u.SlotId);
        }
        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            query = query.Where(filter);
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string ? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }   

            return query.ToList();
        }
        private IQueryable<T> IncludeProperties(IQueryable<T> query, string includeProperties)
        {
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query;
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void RemoveRange(List<T> item)
        {
            dbSet.RemoveRange(item);
        }
    }
}
