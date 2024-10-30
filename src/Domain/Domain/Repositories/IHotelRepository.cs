using Domain.Entities;

namespace Domain.Repositories
{
    public interface IHotelRepository
    {
        Task<Hotel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IEnumerable<Hotel>> GetAllAsync(CancellationToken cancellationToken);
        Task AddAsync(Hotel hotel, CancellationToken cancellationToken);
        Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
