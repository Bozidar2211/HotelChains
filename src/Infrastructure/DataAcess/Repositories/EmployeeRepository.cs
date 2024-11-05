using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;

namespace DataAcess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly RepositoryDbContext _context; // ne moze da se menja kasnije osim u konstruktoru

        public EmployeeRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _context.Employees.FindAsync(new object[] { id }, cancellationToken);

            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} was not found.");        //da ne bude warning za null
            }

            return employee;
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