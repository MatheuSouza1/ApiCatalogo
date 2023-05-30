using ApiCatalogo.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class ApiCatalogoContext : DbContext
    {
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> options) : base(options) { }
    }
}
