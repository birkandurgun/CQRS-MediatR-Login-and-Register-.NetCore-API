using System.Linq.Expressions;

namespace CQRSandMediatR.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
