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
            optionsBuilder.UseSqlServer("YourConnectionString");

            return new RepositoryDbContext(optionsBuilder.Options);
        }
    }

}
