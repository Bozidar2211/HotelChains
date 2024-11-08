using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

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
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.HotelChains.FindAsync(new object[] { id }, cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
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
