using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Services.Exceptions;
using Shared.Helpers;
using Shared.DTOs;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} was not found.");
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee retrieved successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _employeeRepository.GetAllAsync(cancellationToken);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return new ApiResponse<IEnumerable<EmployeeDto>> { Success = true, Message = "Employees retrieved successfully", Data = employeeDtos };
        }

        public async Task<ApiResponse<EmployeeDto>> AddAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            if (employeeDto == null)
            {
                throw new ValidationException("Employee cannot be null.");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.AddAsync(employee, cancellationToken);
            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee added successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<EmployeeDto>> UpdateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            if (employeeDto == null)
            {
                throw new ValidationException("Employee cannot be null.");
            }
            var employee = _mapper.Map<Employee>(employeeDto);
            await _employeeRepository.UpdateAsync(employee, cancellationToken);
            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee updated successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, cancellationToken);
            if (employee == null)
            {
                throw new NotFoundException($"Employee with ID {id} was not found.");
            }
            await _employeeRepository.DeleteAsync(id, cancellationToken);
            return new ApiResponse<bool> { Success = true, Message = "Employee deleted successfully", Data = true };
        }
    }

}
