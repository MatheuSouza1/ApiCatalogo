using ApiCatalogo.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context
{
    public class ApiCatalogoContext : IdentityDbContext
    {
        public DbSet<Category> category { get; set; }
        public DbSet<Product> product { get; set; }
        public ApiCatalogoContext(DbContextOptions<ApiCatalogoContext> options) : base(options) { }
    }
}
