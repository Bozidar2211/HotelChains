using Domain.Entities;

namespace Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Employee employee, CancellationToken cancellationToken);
        Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
