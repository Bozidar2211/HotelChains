using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Service.Abstractions;
using Services.Exceptions;
using Shared.DTOs;
using Shared.Helpers;

namespace Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public HotelService(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<HotelDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with ID {id} was not found.");
            }

            var hotelDto = _mapper.Map<HotelDto>(hotel);

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel retrieved successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<IEnumerable<HotelDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var hotels = await _hotelRepository.GetAllAsync(cancellationToken);
            var hotelDtos = _mapper.Map<IEnumerable<HotelDto>>(hotels);

            return new ApiResponse<IEnumerable<HotelDto>> { Success = true, Message = "Hotels retrieved successfully", Data = hotelDtos };
        }

        public async Task<ApiResponse<HotelDto>> AddAsync(HotelDto hotelDto, CancellationToken cancellationToken)
        {
            if (hotelDto == null)
            {
                throw new ValidationException("Hotel cannot be null.");
            }

            var hotel = _mapper.Map<Hotel>(hotelDto);

            await _hotelRepository.AddAsync(hotel, cancellationToken);

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel added successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<HotelDto>> UpdateAsync(HotelDto hotelDto, CancellationToken cancellationToken)
        {
            if (hotelDto == null)
            {
                throw new ValidationException("Hotel cannot be null.");
            }

            var hotel = _mapper.Map<Hotel>(hotelDto);

            await _hotelRepository.UpdateAsync(hotel, cancellationToken);

            return new ApiResponse<HotelDto> { Success = true, Message = "Hotel updated successfully", Data = hotelDto };
        }

        public async Task<ApiResponse<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotel = await _hotelRepository.GetByIdAsync(id, cancellationToken);

            if (hotel == null)
            {
                throw new NotFoundException($"Hotel with ID {id} was not found.");
            }

            await _hotelRepository.DeleteAsync(id, cancellationToken);

            return new ApiResponse<bool> { Success = true, Message = "Hotel deleted successfully", Data = true };
        }
    }
}
