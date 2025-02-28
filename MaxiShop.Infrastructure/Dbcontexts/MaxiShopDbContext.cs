﻿using MaxiShop.Application.Common;
using MaxiShop.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxiShop.Infrastructure.Dbcontexts
{
    public class MaxiShopDbContext : IdentityDbContext<ApplicationUser>

    {
        public MaxiShopDbContext(DbContextOptions<MaxiShopDbContext>options):base(options)
        {
            
        }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Product> Products { get; set; }
    }
   
}