using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace urbancare_final.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Problem> Problems { get; set; }
        public DbSet<DepartmentMaster> DepartmentMasters { get; set; }

        public DbSet<Resolution> Resolutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Problem>()
                .HasOne(p => p.User)
                .WithMany(u => u.Problems)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resolution>()
                .HasOne(r => r.User)
                .WithMany(u => u.Resolutions)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Problem>()
                .HasOne(p => p.DepartmentMaster)
                .WithMany(d => d.Problems)
                .HasForeignKey(p => p.DepartmentMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resolution>()
                .HasOne(r => r.Department)
                .WithMany(d => d.Resolutions)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resolution>()
                .HasOne(r => r.Problem)
                .WithMany(p => p.Resolutions)
                .HasForeignKey(r => r.ProblemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Department>()
           .HasOne(d => d.DepartmentMaster)
           .WithMany(dm => dm.Departments)
           .HasForeignKey(d => d.DepartmentMasterId)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DepartmentMaster>().HasData(
                new DepartmentMaster { Id = 1, Name = "Sanitation" },
                new DepartmentMaster { Id = 2, Name = "Water Supply" },
                new DepartmentMaster { Id = 3, Name = "Electricity" }
            );
        }


    }
}
