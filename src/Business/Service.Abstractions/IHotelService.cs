using Shared.DTOs;
using Shared.Helpers;


namespace Service.Abstractions
{
    public interface IHotelService
    {
        Task<ApiResponse<HotelDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<HotelDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse<HotelDto>> AddAsync(HotelDto hotelDto, CancellationToken cancellationToken);
        Task<ApiResponse<HotelDto>> UpdateAsync(HotelDto hotelDto, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
