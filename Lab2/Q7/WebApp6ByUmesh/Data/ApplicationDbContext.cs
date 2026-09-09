using Microsoft.EntityFrameworkCore;
using WebApp6ByUmesh.Models;

namespace WebApp6ByUmesh.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}