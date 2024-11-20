using Microsoft.EntityFrameworkCore;
using ApiAspNetCore6.Models;
namespace ApiAspNetCore6.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<tbl_usuario> tbl_usuario { get; set; }




    }
}
