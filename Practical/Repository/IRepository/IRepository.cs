using System.Linq.Expressions;

namespace Practical.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Delete(T entity);

        T GetFirstOrDefault(Expression<Func<T, bool>> filter);

    }
}
