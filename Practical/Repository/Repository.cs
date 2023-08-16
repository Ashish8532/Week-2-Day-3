using Microsoft.EntityFrameworkCore;
using Practical.Data;
using Practical.Repository.IRepository;
using System.Linq.Expressions;

namespace Practical.Repository
{
    /* The following Generic Repository class Implement the Generic IRepository Interface
     * And Here T is going to be a class. While Creating an Instance of the
     * GenericRepository type, we need to specify the Class Name, that is we need to
     * specify the actual class name of the type 
    */
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        // The following Variable is going to hold the DbSet Entity
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;

            // This will set our DbSet to the particular instance of the class that is calling our repository.
            this.dbSet = _context.Set<T>();
        }

        public async Task<T> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }
    }
}
