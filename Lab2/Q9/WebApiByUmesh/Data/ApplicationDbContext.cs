using Microsoft.EntityFrameworkCore;
using WebApiByUmesh.Models;

namespace WebApiByUmesh.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
    }
}