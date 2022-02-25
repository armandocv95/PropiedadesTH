using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropiedadesApp.Data
{
    public class PContext : DbContext
    {
        public PContext(DbContextOptions<PContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Survey> Surveys { get; set; }
    }
}
