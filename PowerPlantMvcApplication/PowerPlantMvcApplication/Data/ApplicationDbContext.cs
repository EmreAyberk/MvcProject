using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PowerPlantMvcApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public virtual DbSet<PowerPlant> PowerPlants { get; set; }
        public virtual DbSet<Electrometer> Electrometers { get; set; }
        public virtual DbSet<PowerPlantUnit> PowerPlantUnits { get; set; }
        public virtual DbSet<Generation> Generations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}