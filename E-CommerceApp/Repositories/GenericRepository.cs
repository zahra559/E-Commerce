using E_CommerceApp.Data;
using E_CommerceApp.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceApp.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDBContext _context;

        protected readonly DbSet<T> _dbSet;

        public GenericRepository(ApplicationDBContext context)
        {
            _context = context;

            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<T> InsertAsync(T Entity)
        {
            await _dbSet.AddAsync(Entity);
            return Entity;
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            _dbSet.Update(Entity);
            return Entity;
        }

        public async Task<T?> DeleteAsync(int Id)
        {
            var entity = await _dbSet.FindAsync(Id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return entity;
            }
            return null;
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
