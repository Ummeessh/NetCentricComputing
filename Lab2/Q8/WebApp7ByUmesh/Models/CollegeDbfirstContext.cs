using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApp7ByUmesh.Models;

public partial class CollegeDbfirstContext : DbContext
{
    public CollegeDbfirstContext()
    {
    }

    public CollegeDbfirstContext(DbContextOptions<CollegeDbfirstContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=CollegeDBFirst;User Id=sa;Password=Umesh@12345#;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC0754896DA3");

            entity.Property(e => e.Faculty).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
