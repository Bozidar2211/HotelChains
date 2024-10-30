using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Entities;
using Service.Abstractions;


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
            return await _employeeRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _employeeRepository.GetAllAsync(cancellationToken);
        }

        public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeRepository.AddAsync(employee, cancellationToken);
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
        {
            await _employeeRepository.UpdateAsync(employee, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _employeeRepository.DeleteAsync(id, cancellationToken);
        }
    }
}
