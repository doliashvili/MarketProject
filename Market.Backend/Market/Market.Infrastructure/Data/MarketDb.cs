using Market.ReadModels.Models;
using Microsoft.EntityFrameworkCore;
using ReadModelRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ReadModelRepository.MSSQL;

namespace Market.Infrastructure.Data
{
   public class MarketDb : AppDbContext
    {
        public MarketDb(DbContextOptions<MarketDb> options)
        : base(options)
        {
            
        }

        public DbSet<ProductReadModel> ProductReadModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ProductReadModel>().OwnsMany(x=> x.Images);
            modelBuilder.Entity<ProductReadModel>().OwnsOne(x=> x.Weight);

        }
    }
}
