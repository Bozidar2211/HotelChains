using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Shared.DTOs;
using Shared.Helpers;

namespace Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HotelDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                return new ApiResponse<HotelDto> { Success = false, Message = $"Hotel with ID {id} was not found." };
            }

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel retrieved successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<IEnumerable<HotelDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync(cancellationToken);
            var hotelDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);

            return new ApiResponse<IEnumerable<HotelDto>> { Success = true, Message = "Hotels retrieved successfully", Data = hotelDtos };
        }

        public async Task<ApiResponse<HotelDto>> AddAsync(HotelDto hotelDto, CancellationToken cancellationToken)
        {
            if (hotelDto == null)
            {
                return new ApiResponse<HotelDto> { Success = false, Message = "Hotel cannot be null." };
            }

            var hotel = _mapper.Map<Hotel>(hotelDto);

            await _unitOfWork.Hotels.AddAsync(hotel, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel added successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<HotelDto>> UpdateAsync(HotelDto hotelDto, CancellationToken cancellationToken)
        {
            if (hotelDto == null)
            {
                return new ApiResponse<HotelDto> { Success = false, Message = "Hotel cannot be null." };
            }

            var hotel = _mapper.Map<Hotel>(hotelDto);

            await _unitOfWork.Hotels.UpdateAsync(hotel, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel updated successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotel = await _unitOfWork.Hotels.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                return new ApiResponse<bool> { Success = false, Message = $"Hotel with ID {id} was not found." };
            }

            await _unitOfWork.Hotels.DeleteAsync(id, cancellationToken);
            await _unitOfWork.CompleteAsync();

            return new ApiResponse<bool> { Success = true, Message = "Hotel deleted successfully", Data = true };
        }
    }
}