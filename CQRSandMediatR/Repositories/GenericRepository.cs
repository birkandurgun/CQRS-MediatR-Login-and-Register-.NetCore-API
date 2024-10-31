using CQRSandMediatR.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CQRSandMediatR.Repositories
{
    public class GenericRepository<T>: IGenericRepository<T> where T: class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate) => await _dbSet.SingleOrDefaultAsync(predicate);
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);
        public void Remove(T entity) => _dbSet.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
