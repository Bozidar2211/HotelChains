using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public interface IRepositoryDbContext
    {
        DbSet<Employee> Employees { get; set; }
        DbSet<Hotel> Hotels { get; set; }
        DbSet<HotelChain> HotelChains { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
