using MaxiShop.Domain.Common;
using MaxiShop.Domain.Contracts;
using MaxiShop.Infrastructure.Dbcontexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        protected readonly MaxiShopDbContext _maxi;
        public GenericRepository(MaxiShopDbContext maxi)
        {
            _maxi=maxi;
        }
        public async Task<T> CreateAsync(T entity)
        {
            var addedEntity = await _maxi.Set<T>().AddAsync(entity);
            await _maxi.SaveChangesAsync();
            return addedEntity.Entity;

        }

        public async Task DeleteAsync(T entity)
        {
            _maxi.Remove(entity);
            await _maxi.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _maxi.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> condition)
        {
           return await _maxi.Set<T>().AsNoTracking().FirstOrDefaultAsync(condition);
        }
        
        
    }
}
