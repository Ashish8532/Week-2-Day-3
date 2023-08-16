using Practical.Data;
using Practical.Models;
using Practical.Repository.IRepository;

namespace Practical.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> Update(Product obj)
        {
            _context.Products.Update(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
    }
}
