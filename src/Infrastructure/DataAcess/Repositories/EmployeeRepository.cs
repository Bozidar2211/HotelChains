using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace DataAcess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RepositoryDbContext _context;

        public EmployeeRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Employees.FindAsync(new object[] { id }, cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);

            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
