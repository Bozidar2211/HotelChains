﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAcess
{
    public class RepositoryDbContext : DbContext
    {
        public RepositoryDbContext(DbContextOptions<RepositoryDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelChain> HotelChains { get; set; }

#if !DEBUG
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PO97NUT;Initial Catalog=HotelChainsDb;Integrated Security=True;Trust Server Certificate=True");
        }
#endif

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