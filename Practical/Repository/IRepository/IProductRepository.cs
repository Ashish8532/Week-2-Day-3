using Practical.Models;

namespace Practical.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Update(Product obj);
    }
}
