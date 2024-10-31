using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Services.Exceptions;


namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Employee> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} was not found.");
            }
            return employee;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllAsync(cancellationToken);
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            if (employee == null)
            {
                throw new ValidationException("Employee cannot be null.");
            }
            await _employeeRepository.AddAsync(employee, cancellationToken);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            if (employee == null)
            {
                throw new ValidationException("Employee cannot be null.");
            }
            await _employeeRepository.UpdateAsync(employee, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} was not found.");
            }
            await _employeeRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
