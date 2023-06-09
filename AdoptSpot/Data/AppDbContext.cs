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
        
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Adoption> Adoptions { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<ImageModel> Images { get; set; }
        public DbSet<Vaccination> Vaccinations { get; set; }
        public DbSet<MedicalTreatment> MedicalTreatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Adoption>()
                .HasOne(a => a.Pet)
                .WithMany(p => p.Adoptions)
                .HasForeignKey(a => a.PetId);

            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Pet)
                .WithOne(p => p.MedicalRecord)
                .HasForeignKey<MedicalRecord>(mr => mr.PetId);

            modelBuilder.Entity<ImageModel>()
                .HasOne(mh => mh.Pet)
                .WithMany(p => p.Images)
                .HasForeignKey(mh => mh.PetId);

            modelBuilder.Entity<MedicalTreatment>()
                .HasOne(mt => mt.MedicalRecord)
                .WithMany(mr => mr.MedicalTreatments)
                .HasForeignKey(mt => mt.MedicalRecordId);

            modelBuilder.Entity<Vaccination>()
                    .HasOne(v => v.MedicalRecord)
                    .WithMany(mr => mr.Vaccinations)
                    .HasForeignKey(v => v.MedicalRecordId);

        }
    }
}
