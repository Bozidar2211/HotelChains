using DataAcess;
using DataAcess.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class EmployeeRepositoryTests
    {
        private EmployeeRepository _repository;
        private RepositoryDbContext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RepositoryDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")              //izolovana test baza
                .Options;

            _context = new RepositoryDbContext(options);
            _repository = new EmployeeRepository(_context);
        }

        [TearDown]
        public void TearDown()                  //Automatko brisanje baze posle svakog testa
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private async Task<Hotel> CreateHotelAsync()        //kreiranje i dodavanje entiteta u bazu
        {
            var hotelChain = new HotelChain
            {
                Id = Guid.NewGuid(),
                Name = "Test Hotel Chain",
                Hotels = new List<Hotel>()
            };

            var hotel = new Hotel
            {
                Id = Guid.NewGuid(),
                Name = "Test Hotel",
                YearOpened = 2000,
                NumberOfEmployees = 100,
                NumberOfRooms = 50,
                HotelChainId = hotelChain.Id,
                HotelChain = hotelChain
            };

            hotelChain.Hotels.Add(hotel);

            await _context.HotelChains.AddAsync(hotelChain);
            await _context.Hotels.AddAsync(hotel);
            await _context.SaveChangesAsync();

            return hotel;           //vraca potrebni objekat
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {

            //Arrange

            var hotel = await CreateHotelAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            //Act

            var result = await _repository.GetByIdAsync(employee.Id, CancellationToken.None);

            //Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(employee.Id));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {

            //Arrange

            var hotel = await CreateHotelAsync();

            var employees = new List<Employee>
            {
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    HotelId = hotel.Id,
                    Hotel = hotel,
                    Position = "Manager"
                },
                new Employee
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Jane",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1992, 2, 2),
                    HotelId = hotel.Id,
                    Hotel = hotel,
                    Position = "Receptionist"
                }
            };
            await _context.Employees.AddRangeAsync(employees);
            await _context.SaveChangesAsync();


            //Act

            var result = await _repository.GetAllAsync(CancellationToken.None);

            //Assert

            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task AddAsync_ShouldAddEmployee()
        {
            //Arrange

            var hotel = await CreateHotelAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };

            await _repository.AddAsync(employee, CancellationToken.None);

            //Act

            var result = await _context.Employees.FindAsync(employee.Id);

            //Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(employee.Id));
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {

            //Arrange

            var hotel = await CreateHotelAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            employee.FirstName = "Johnathan";
            await _repository.UpdateAsync(employee, CancellationToken.None);

            //Act

            var result = await _context.Employees.FindAsync(employee.Id);

            //Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.FirstName, Is.EqualTo("Johnathan"));
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {

            //Arrange
            var hotel = await CreateHotelAsync();

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            await _repository.DeleteAsync(employee.Id, CancellationToken.None);

            //Act

            var result = await _context.Employees.FindAsync(employee.Id);

            //Assert

            Assert.That(result, Is.Null);
        }
    }
}
