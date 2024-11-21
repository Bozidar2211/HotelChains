using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DTOs;
using Shared.Helpers;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<EmployeeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id, cancellationToken);

            if (employee == null)
            {
                return new ApiResponse<EmployeeDto> { Success = false, Message = $"Employee with ID {id} was not found." };
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee retrieved successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<List<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var employees = await _unitOfWork.Employees.GetAllAsync(cancellationToken);
            var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);


            return new ApiResponse<List<EmployeeDto>> { Success = true, Message = "Employees retrieved successfully", Data = employeeDtos };
        }

        public async Task<ApiResponse<EmployeeDto>> AddAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            if (employeeDto == null)
            {
                return new ApiResponse<EmployeeDto> { Success = false, Message = "Employee cannot be null." };
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            await _unitOfWork.Employees.AddAsync(employee, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee added successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<EmployeeDto>> UpdateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            if (employeeDto == null)
            {
                return new ApiResponse<EmployeeDto> { Success = false, Message = "Employee cannot be null." };
            }

            var employee = _mapper.Map<Employee>(employeeDto);

            await _unitOfWork.Employees.UpdateAsync(employee, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<EmployeeDto> { Success = true, Message = "Employee updated successfully", Data = employeeDto };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id, cancellationToken);

            if (employee == null)
            {
                return new ApiResponse<bool> { Success = false, Message = $"Employee with ID {id} was not found." };
            }

            await _unitOfWork.Employees.DeleteAsync(id, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<bool> { Success = true, Message = "Employee deleted successfully", Data = true };
        }
    }
}