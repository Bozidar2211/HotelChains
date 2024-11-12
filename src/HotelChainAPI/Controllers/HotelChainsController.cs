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
    public class HotelChainsController : ControllerBase
    {
        private readonly IHotelChainService _hotelChainService;

        public HotelChainsController(IHotelChainService hotelChainService)
        {
            _hotelChainService = hotelChainService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotelChain(Guid id, CancellationToken cancellationToken)
        {
            var response = await _hotelChainService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotelChains(CancellationToken cancellationToken)
        {
            var response = await _hotelChainService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddHotelChain([FromBody] HotelChainDto hotelChainDto, CancellationToken cancellationToken)
        {
            var response = await _hotelChainService.AddAsync(hotelChainDto, cancellationToken);

            return CreatedAtAction(nameof(GetHotelChain), new { id = hotelChainDto.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotelChain(Guid id, [FromBody] HotelChainDto hotelChainDto, CancellationToken cancellationToken)
        {
            if (id != hotelChainDto.Id)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = "ID mismatch" });
            }

            var response = await _hotelChainService.UpdateAsync(hotelChainDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelChain(Guid id, CancellationToken cancellationToken)
        {
            var response = await _hotelChainService.DeleteAsync(id, cancellationToken);

            return Ok(response);
        }
    }

}
