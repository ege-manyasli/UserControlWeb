using Microsoft.EntityFrameworkCore;
using UserControlWeb.Models;

namespace UserControlWeb.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        

    }
}
