using Practical.Data;
using Practical.Models;
using Practical.Repository.IRepository;

namespace Practical.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Category> Update(Category obj)
        {
            _context.Categories.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
