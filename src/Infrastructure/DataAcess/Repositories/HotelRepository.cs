using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DataAcess.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private RepositoryDbContext _context;

        public HotelRepository(RepositoryDbContext context)
        {
            _context = context;
        }

        public async Task<Hotel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Hotels.FindAsync(new object[] { id }, cancellationToken);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Hotels.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            await _context.Hotels.AddAsync(hotel, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken)
        {
            _context.Hotels.Update(hotel);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var hotel = await _context.Hotels.FindAsync(new object[] { id }, cancellationToken);

            if (hotel != null)
            {
                _context.Hotels.Remove(hotel);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
