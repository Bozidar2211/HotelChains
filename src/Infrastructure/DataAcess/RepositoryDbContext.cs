using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Domain.Entities;

namespace DataAcess
{
    public class RepositoryDbContext : DbContext
    {  
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelChain> HotelChains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PO97NUT;Initial Catalog=HotelChainsDb;Integrated Security=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //relationships and constraints 
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.HotelChain)
                .WithMany(hc => hc.Hotels)
                .HasForeignKey(h => h.HotelChainId);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Hotel)
                .WithMany(h => h.Employees)
                .HasForeignKey(e => e.HotelId);
        }
    }
}
