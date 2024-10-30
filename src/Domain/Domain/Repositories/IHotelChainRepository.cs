using Domain.Entities;

namespace Domain.Repositories
{
    public interface IHotelChainRepository
    {
        Task<HotelChain> GetByIdAsync(Guid id, CancellationToken cancellationtoken = default);     //trazi po ID-u
        Task<IEnumerable<HotelChain>> GetAllAsync(CancellationToken cancellationtoken = default);        //trazi sve
        Task AddAsync(HotelChain hotelChain, CancellationToken cancellationtoken = default);
        Task UpdateAsync(HotelChain hotelChain, CancellationToken cancellationtoken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationtoken = default);
    }
}
