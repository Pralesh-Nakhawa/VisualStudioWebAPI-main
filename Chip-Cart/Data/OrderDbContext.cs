using Chip_Cart.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chip_Cart.Data
{
    public class OrderDbContext:DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }
        public DbSet<OrderModel> orderModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>().ToTable("orderdetails");
        }
    }
}
