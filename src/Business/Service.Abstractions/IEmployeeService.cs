using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Abstractions
{
    public interface IEmployeeService
    {
        Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Employee employee, CancellationToken cancellationToken);
        Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
