using Domain.Repositories;

namespace DataAcess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RepositoryDbContext _context;

        public UnitOfWork(RepositoryDbContext context, IEmployeeRepository employeeRepository, IHotelRepository hotelRepository, IHotelChainRepository hotelChainRepository)
        {
            _context = context;
            Employees = employeeRepository;
            Hotels = hotelRepository;
            HotelChains = hotelChainRepository;
        }

        public IEmployeeRepository Employees { get; }
        public IHotelRepository Hotels { get; }
        public IHotelChainRepository HotelChains { get; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}


