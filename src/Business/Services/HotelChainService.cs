using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DTOs;
using Shared.Helpers;

namespace Services
{
    public class HotelChainService : IHotelChainService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelChainService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HotelChainDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotelChain = await _unitOfWork.HotelChains.GetByIdAsync(id, cancellationToken);

            if (hotelChain == null)
            {
                return new ApiResponse<HotelChainDto> { Success = false, Message = $"HotelChain with ID {id} was not found." };
            }

            var hotelChainDto = _mapper.Map<HotelChainDto>(hotelChain);

            return new ApiResponse<HotelChainDto> { Success = true, Message = "HotelChain retrieved successfully", Data = hotelChainDto };
        }

        public async Task<ApiResponse<IEnumerable<HotelChainDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var hotelChains = await _unitOfWork.HotelChains.GetAllAsync(cancellationToken);
            var hotelChainDtos = _mapper.Map<IEnumerable<HotelChainDto>>(hotelChains);

            return new ApiResponse<IEnumerable<HotelChainDto>> { Success = true, Message = "HotelChains retrieved successfully", Data = hotelChainDtos };
        }

        public async Task<ApiResponse<HotelChainDto>> AddAsync(HotelChainDto hotelChainDto, CancellationToken cancellationToken)
        {
            if (hotelChainDto == null)
            {
                return new ApiResponse<HotelChainDto> { Success = false, Message = "HotelChain cannot be null." };
            }

            var hotelChain = _mapper.Map<HotelChain>(hotelChainDto);

            await _unitOfWork.HotelChains.AddAsync(hotelChain, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<HotelChainDto> { Success = true, Message = "HotelChain added successfully", Data = hotelChainDto };
        }

        public async Task<ApiResponse<HotelChainDto>> UpdateAsync(HotelChainDto hotelChainDto, CancellationToken cancellationToken)
        {
            if (hotelChainDto == null)
            {
                return new ApiResponse<HotelChainDto> { Success = false, Message = "HotelChain cannot be null." };
            }

            var hotelChain = _mapper.Map<HotelChain>(hotelChainDto);

            await _unitOfWork.HotelChains.UpdateAsync(hotelChain, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<HotelChainDto> { Success = true, Message = "HotelChain updated successfully", Data = hotelChainDto };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotelChain = await _unitOfWork.HotelChains.GetByIdAsync(id, cancellationToken);

            if (hotelChain == null)
            {
                return new ApiResponse<bool> { Success = false, Message = $"HotelChain with ID {id} was not found." };
            }

            await _unitOfWork.HotelChains.DeleteAsync(id, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<bool> { Success = true, Message = "HotelChain deleted successfully", Data = true };
        }
    }
}