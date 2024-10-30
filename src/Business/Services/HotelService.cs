﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Service.Abstractions;
using Domain.Repositories;

namespace Services
{
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelService(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        public async Task<Hotel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _hotelRepository.GetByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _hotelRepository.GetAllAsync(cancellationToken);
        }

        public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            await _hotelRepository.AddAsync(hotel, cancellationToken);
        }

        public async Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            await _hotelRepository.UpdateAsync(hotel, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await _hotelRepository.DeleteAsync(id, cancellationToken);
        }
    }
}