using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class RepositoryDbContextFactory : IDesignTimeDbContextFactory<RepositoryDbContext>
    {
        public RepositoryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryDbContext>();
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-PO97NUT;Initial Catalog=HotelChainsDb;Integrated Security=True;Trust Server Certificate=True");

            return new RepositoryDbContext(optionsBuilder.Options);
        }
    }

}
