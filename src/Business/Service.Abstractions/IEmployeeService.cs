﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Shared.DTOs;
using Shared.Helpers;

namespace Service.Abstractions
{
    public interface IEmployeeService
    {
        Task<ApiResponse<EmployeeDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse<EmployeeDto>> AddAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
        Task<ApiResponse<EmployeeDto>> UpdateAsync(EmployeeDto employeeDto, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
