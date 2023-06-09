using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdoptSpot.Areas.Identity.Data;
using AdoptSpot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AdoptSpot.Data
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Ignore<Pet>();
            builder.Ignore<Adoption>();
            builder.Ignore<MedicalRecord>();
            builder.Ignore<ImageModel>();
            builder.Ignore<Vaccination>();
            builder.Ignore<MedicalTreatment>();
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
