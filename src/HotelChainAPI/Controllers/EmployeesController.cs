using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using Shared.DTOs;
using Shared.Helpers;

namespace HotelChainAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee(Guid id, CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees(CancellationToken cancellationToken)
        {
            var response = await _employeeService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            var response = await _employeeService.AddAsync(employeeDto, cancellationToken);

            return CreatedAtAction(nameof(GetEmployee), new { id = employeeDto.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
        {
            if (id != employeeDto.Id)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = "ID mismatch" });
            }
            var response = await _employeeService.UpdateAsync(employeeDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id, CancellationToken cancellationToken)
        {
            var response = await _employeeService.DeleteAsync(id, cancellationToken);

            return Ok(response);
        }
    }
}
