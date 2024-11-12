using DataAcess;
using DataAcess.Repositories;
using Domain.Entities;
using Moq;
using NUnit.Framework;
using NUnit;
using NUnitTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace NUnitTests
{
    [TestFixture]
    public class EmployeeRepositoryTest
    {
        private Mock<RepositoryDbContext> _contextMock;     //objekat koji mockujemo
        private EmployeeRepository _repository;     //Klasa u kojoj testiramo

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<RepositoryDbContext>();
            _repository = new EmployeeRepository(_contextMock.Object);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            //Arrange
            var employeeId = Guid.NewGuid();
            var hotelChain = new HotelChain { Id = Guid.NewGuid(), Name = "Chain Test", YearEstablished = 2000, Hotels = new List<Hotel>() };
            var hotel = new Hotel { Id = Guid.NewGuid(), Name = "Hotel Test", YearOpened = 2010, NumberOfEmployees = 50, NumberOfRooms = 100, HotelChainId = hotelChain.Id, HotelChain = hotelChain };
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };

            var dbSetMock = new Mock<DbSet<Employee>>();

            dbSetMock.Setup(m => m.FindAsync(new Object[] { employeeId }, It.IsAny<CancellationToken>())).ReturnsAsync(employee);

            _contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            //Act

            var result = await _repository.GetByIdAsync(employeeId, CancellationToken.None);

            //Assert

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(employeeId));         //nova verzija NUnita
        }
        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            //Arrange

            var hotelChain = new HotelChain { Id = Guid.NewGuid(), Name = "Chain Test", YearEstablished = 2000, Hotels = new List<Hotel>() };
            var hotel = new Hotel { Id = Guid.NewGuid(), Name = "Hotel Test", YearOpened = 2010, NumberOfEmployees = 50, NumberOfRooms = 100, HotelChainId = hotelChain.Id, HotelChain = hotelChain };
            var employees = new List<Employee>
            {
                new Employee { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", DateOfBirth = new DateTime(1990, 1, 1), HotelId = hotel.Id, Hotel = hotel, Position = "Manager" },
                new Employee { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith", DateOfBirth = new DateTime(1992, 2, 2), HotelId = hotel.Id, Hotel = hotel, Position = "Receptionist" }
            };

            var dbSetMock = new Mock<DbSet<Employee>>();

            dbSetMock.Setup(m => m.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(employees);

            _contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            //Act

            var result = await _repository.GetAllAsync(CancellationToken.None);

            //Assert
            Assert.That(result.Count, Is.EqualTo(2));

        }
        [Test]
        public async Task AddAsync_ShouldAddEmployee()
        {
            // Arrange

            var hotelChain = new HotelChain { Id = Guid.NewGuid(), Name = "Chain Test", YearEstablished = 2000, Hotels = new List<Hotel>() };
            var hotel = new Hotel { Id = Guid.NewGuid(), Name = "Hotel Test", YearOpened = 2010, NumberOfEmployees = 50, NumberOfRooms = 100, HotelChainId = hotelChain.Id, HotelChain = hotelChain };
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

            var entityEntryMock = new Mock<EntityEntry<Employee>>();
            var dbSetMock = new Mock<DbSet<Employee>>();

            dbSetMock.Setup(m => m.AddAsync(employee, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(entityEntryMock.Object);

            _contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            // Act

            await _repository.AddAsync(employee, CancellationToken.None);

            // Assert

            dbSetMock.Verify(m => m.AddAsync(employee, It.IsAny<CancellationToken>()), Times.Once);

            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var hotelChain = new HotelChain { Id = Guid.NewGuid(), Name = "Chain Test", YearEstablished = 2000, Hotels = new List<Hotel>() };
            var hotel = new Hotel { Id = Guid.NewGuid(), Name = "Hotel Test", YearOpened = 2010, NumberOfEmployees = 50, NumberOfRooms = 100, HotelChainId = hotelChain.Id, HotelChain = hotelChain };
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

            var dbSetMock = new Mock<DbSet<Employee>>();
            dbSetMock.Setup(m => m.Update(employee));

            _contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            // Act
            await _repository.UpdateAsync(employee, CancellationToken.None);

            // Assert
            dbSetMock.Verify(m => m.Update(employee), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {
            // Arrange
            var employeeId = Guid.NewGuid();
            var hotelChain = new HotelChain { Id = Guid.NewGuid(), Name = "Chain Test", YearEstablished = 2000, Hotels = new List<Hotel>() };
            var hotel = new Hotel { Id = Guid.NewGuid(), Name = "Hotel Test", YearOpened = 2010, NumberOfEmployees = 50, NumberOfRooms = 100, HotelChainId = hotelChain.Id, HotelChain = hotelChain };
            var employee = new Employee
            {
                Id = employeeId,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                HotelId = hotel.Id,
                Hotel = hotel,
                Position = "Manager"
            };

            var dbSetMock = new Mock<DbSet<Employee>>();
            dbSetMock.Setup(m => m.FindAsync(new object[] { employeeId }, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(employee);
            dbSetMock.Setup(m => m.Remove(employee));

            _contextMock.Setup(c => c.Employees).Returns(dbSetMock.Object);

            // Act
            await _repository.DeleteAsync(employeeId, CancellationToken.None);

            // Assert
            dbSetMock.Verify(m => m.Remove(employee), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}