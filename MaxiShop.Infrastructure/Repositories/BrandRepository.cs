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
    public class BrandRepository:GenericRepository<Brand>,IBrandRepository
    {
        public BrandRepository(MaxiShopDbContext dbContext) : base(dbContext)
        {

        }

        public async Task UpdateAsync(Brand brand)
        {
            _maxi.Update(brand);
            await _maxi.SaveChangesAsync();
        }
    }
}
