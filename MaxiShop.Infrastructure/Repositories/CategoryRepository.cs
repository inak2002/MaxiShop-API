using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.Dbcontexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(MaxiShopDbContext dbContext):base(dbContext)
        {
            
        }
        public async Task UpdateAsync(Category category)
        {
            _maxi.Update(category);
            await _maxi.SaveChangesAsync();
        }
    }
}
