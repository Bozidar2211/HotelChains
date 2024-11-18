using DataAcess;
using DataAcess.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using MockQueryable.Moq;

namespace NUnitTests.RepositoryTest
{
    public class EmployeeRepositoryTests
    {
        private Mock<IRepositoryDbContext> _contextMock;
        private Mock<DbSet<Employee>> _dbSetMock;
        private EmployeeRepository _repository;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<IRepositoryDbContext>();
            _dbSetMock = new Mock<DbSet<Employee>>();

            _contextMock.Setup(c => c.Employees).Returns(_dbSetMock.Object);
            _repository = new EmployeeRepository(_contextMock.Object);
        }

       /* [TearDown]
        public void TearDown()
        {
            _contextMock = null;
            _dbSetMock = null;
            _repository = null;
        }*/

        private List<Employee> GetTestEmployees()
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

            return new List<Employee>
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
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employees = GetTestEmployees();
            var employee = employees.First();
            _dbSetMock.Setup(m => m.FindAsync(new object[] { employee.Id }, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(employee);

            // Act
            var result = await _repository.GetByIdAsync(employee.Id, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(employee.Id));
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employees = GetTestEmployees().AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.Employees).Returns(employees.Object);

            // Act
            var result = await _repository.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task AddAsync_ShouldAddEmployee()
        {
            // Arrange
            var employees = GetTestEmployees();
            var employee = employees.First();
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            _dbSetMock.Setup(m => m.AddAsync(employee, It.IsAny<CancellationToken>()))
                      .ReturnsAsync((EntityEntry<Employee>)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            // Act
            await _repository.AddAsync(employee, CancellationToken.None);

            // Assert
            _dbSetMock.Verify(m => m.AddAsync(employee, It.IsAny<CancellationToken>()), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employees = GetTestEmployees();
            var employee = employees.First();
            _dbSetMock.Setup(m => m.Update(employee));

            // Act
            await _repository.UpdateAsync(employee, CancellationToken.None);

            // Assert
            _dbSetMock.Verify(m => m.Update(employee), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {
            // Arrange
            var employees = GetTestEmployees();
            var employee = employees.First();
            _dbSetMock.Setup(m => m.FindAsync(new object[] { employee.Id }, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(employee);
            _dbSetMock.Setup(m => m.Remove(employee));

            // Act
            await _repository.DeleteAsync(employee.Id, CancellationToken.None);

            // Assert
            _dbSetMock.Verify(m => m.Remove(employee), Times.Once);
            _contextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}