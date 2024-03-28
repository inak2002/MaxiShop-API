using MaxiShop.Domain.Contracts;
using MaxiShop.Domain.Models;
using MaxiShop.Infrastructure.Dbcontexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(MaxiShopDbContext maxi) : base(maxi)
        {
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _maxi.Products.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().ToListAsync();
        }

       public async Task<Product> GetDetailsAsync(int id)
        {
            return  _maxi.Products.Include(x => x.Category).Include(x => x.Brand).AsNoTracking().FirstOrDefault(x=>x.Id==id);
        }

        public async Task UpdateAsync(Product product)
        {
            _maxi.Update(product);
            await _maxi.SaveChangesAsync();
        }

        
    }
}
