using AdoptSpot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdoptSpot.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.User)
                .WithMany(u => u.Adoptions)
                .HasForeignKey(a => a.UserId);

            modelBuilder.Entity<Adoption>()
             .HasOne(a => a.Pet)
             .WithMany(p => p.Adoptions)
             .HasForeignKey(a => a.PetId);

            modelBuilder.Entity<MedicalHistory>()
                .HasOne(mh => mh.Pet)
                .WithMany(p => p.MedicalHistories)
                .HasForeignKey(mh => mh.PetId);

            modelBuilder.Entity<Image>()
               .HasOne(mh => mh.Pet)
               .WithMany(p => p.Images)
               .HasForeignKey(mh => mh.PetId);
        }
    }
}
