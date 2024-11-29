using IDP.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Infra.Data
{
    public class ShopQueryDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ShopQueryDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("QueryDBConection"));
        }
        public DbSet<User> Tbl_User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

