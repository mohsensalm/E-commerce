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
    public class ShopComandDBContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ShopComandDBContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(_configuration.GetConnectionString("CommandDBConection"));
        }
        public DbSet<User> Tbl_User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
