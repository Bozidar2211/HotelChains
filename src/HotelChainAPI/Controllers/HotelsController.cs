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
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHotel(Guid id, CancellationToken cancellationToken)
        {
            var response = await _hotelService.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHotels(CancellationToken cancellationToken)
        {
            var response = await _hotelService.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddHotel([FromBody] HotelDto hotelDto, CancellationToken cancellationToken)
        {
            var response = await _hotelService.AddAsync(hotelDto, cancellationToken);

            return CreatedAtAction(nameof(GetHotel), new { id = hotelDto.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHotel(Guid id, [FromBody] HotelDto hotelDto, CancellationToken cancellationToken)
        {
            if (id != hotelDto.Id)
            {
                return BadRequest(new ApiResponse<object> { Success = false, Message = "ID mismatch" });
            }

            var response = await _hotelService.UpdateAsync(hotelDto, cancellationToken);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(Guid id, CancellationToken cancellationToken)
        {
            var response = await _hotelService.DeleteAsync(id, cancellationToken);

            return Ok(response);
        }
    }

}
