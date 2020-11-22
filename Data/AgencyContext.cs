using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KareAjans.Models;

namespace KareAjans.Data
{
    public class AgencyContext : DbContext
    {
        public AgencyContext (DbContextOptions<AgencyContext> options)
            : base(options)
        {
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Actor>().ToTable("Actor");
            modelBuilder.Entity<Staff>().ToTable("Staff");
            modelBuilder.Entity<Contract>().ToTable("Contract");
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
