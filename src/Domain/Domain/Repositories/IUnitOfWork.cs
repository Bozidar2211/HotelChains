namespace Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IHotelRepository Hotels { get; }
        IHotelChainRepository HotelChains { get; }
        Task<int> CompleteAsync();
    }
}