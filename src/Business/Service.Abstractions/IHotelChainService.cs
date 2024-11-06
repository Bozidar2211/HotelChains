using Shared.DTOs;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstractions
{
    public interface IHotelChainService
    {
        Task<ApiResponse<HotelChainDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ApiResponse<IEnumerable<HotelChainDto>>> GetAllAsync(CancellationToken cancellationToken);
        Task<ApiResponse<HotelChainDto>> AddAsync(HotelChainDto hotelChainDto, CancellationToken cancellationToken);
        Task<ApiResponse<HotelChainDto>> UpdateAsync(HotelChainDto hotelChainDto, CancellationToken cancellationToken);
        Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
