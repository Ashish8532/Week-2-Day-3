using Practical.Models;

namespace Practical.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> Update(Category obj);
    }
}
