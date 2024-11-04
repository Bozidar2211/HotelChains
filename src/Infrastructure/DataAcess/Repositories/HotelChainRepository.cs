using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Services.Exceptions;

namespace DataAcess.Repositories
{
    public class HotelChainRepository : IHotelChainRepository
    {
        private readonly RepositoryDbContext _context;

        public HotelChainRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public async Task<HotelChain> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotelChain = await _context.HotelChains.FindAsync(new object[] { id }, cancellationToken);

            if (hotelChain == null)
            {
                throw new NotFoundException($"Hotel chain with ID {id} was not found");
            }

            return hotelChain;
        }

        public async Task<IEnumerable<HotelChain>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.HotelChains.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(HotelChain hotelChain, CancellationToken cancellationToken)
        {
            await _context.HotelChains.AddAsync(hotelChain, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(HotelChain hotelChain, CancellationToken cancellationToken)
        {
            _context.HotelChains.Update(hotelChain);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotelChain = await _context.HotelChains.FindAsync(new object[] { id }, cancellationToken);

            if (hotelChain != null)
            {
                _context.HotelChains.Remove(hotelChain);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
