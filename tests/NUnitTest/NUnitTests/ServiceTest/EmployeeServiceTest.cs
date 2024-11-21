using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using Service.Abstractions;
using Services;
using Shared.DTOs;

namespace NUnitTests.ServiceTest
{
    public class EmployeeServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private IEmployeeService _employeeService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>(MockBehavior.Strict);
            _mapperMock = new Mock<IMapper>();
            _employeeService = new EmployeeService(_unitOfWorkMock.Object, _mapperMock.Object);

            // Setup CompleteAsync method
            _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);
        }

        private Employee CreateTestEmployees()
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
            hotel.Employees.Add(employee);

            hotelChain.Hotels.Add(hotel);

            return employee;
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var employee = CreateTestEmployees();
            var employeeDto = new EmployeeDto { Id = employee.Id, FirstName = employee.FirstName, LastName = employee.LastName, Position = employee.Position };

            _unitOfWorkMock.Setup(uow => uow.Employees.GetByIdAsync(employee.Id, It.IsAny<CancellationToken>()))
                            .ReturnsAsync(employee);


            _mapperMock.Setup(m => m.Map<EmployeeDto>(employee)).Returns(employeeDto);

            // Act
            var result = await _employeeService.GetByIdAsync(employee.Id, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Message, Is.EqualTo("Employee retrieved successfully"));
                Assert.That(result.Data, Is.EqualTo(employeeDto));
            });
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllEmployees()
        {
            // Arrange
            var employee = CreateTestEmployees();
            var employees = new List<Employee> { employee };
            var employeeDtos = new List<EmployeeDto> { new EmployeeDto { Id = employee.Id, FirstName = employee.FirstName, LastName = employee.LastName, Position = employee.Position } };

            _unitOfWorkMock.Setup(uow => uow.Employees.GetAllAsync(It.IsAny<CancellationToken>()))
                           .ReturnsAsync(employees);

            _mapperMock.Setup(m => m.Map<List<EmployeeDto>>(employees)).Returns(employeeDtos);

            // Act
            var result = await _employeeService.GetAllAsync(CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Message, Is.EqualTo("Employees retrieved successfully"));
                Assert.That(result.Data, Is.EqualTo(employeeDtos));
            });
        }

        [Test]
        public async Task AddAsync_ShouldAddEmployee()
        {
            // Arrange
            var employee = CreateTestEmployees();
            var employeeDto = new EmployeeDto { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Position = "Cook" };

            _mapperMock.Setup(m => m.Map<Employee>(employeeDto)).Returns(employee);

            _unitOfWorkMock.Setup(uow => uow.Employees.AddAsync(employee, It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask); // Setup for AddAsync
            // Act
            var result = await _employeeService.AddAsync(employeeDto, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Message, Is.EqualTo("Employee added successfully"));
                Assert.That(result.Data, Is.EqualTo(employeeDto));
            });
            _unitOfWorkMock.Verify(uow => uow.Employees.AddAsync(employee, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var employee = CreateTestEmployees();
            var employeeDto = new EmployeeDto { Id = Guid.NewGuid(), FirstName = "John", LastName = "Doe", Position = "Bartender" };


            _mapperMock.Setup(m => m.Map<Employee>(employeeDto)).Returns(employee);

            _unitOfWorkMock.Setup(uow => uow.Employees.UpdateAsync(employee, It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask); // Setup for UpdateAsync
            // Act
            var result = await _employeeService.UpdateAsync(employeeDto, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Message, Is.EqualTo("Employee updated successfully"));
                Assert.That(result.Data, Is.EqualTo(employeeDto));
            });
            _unitOfWorkMock.Verify(uow => uow.Employees.UpdateAsync(employee, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {
            // Arrange
            var employee = CreateTestEmployees();

            _unitOfWorkMock.Setup(uow => uow.Employees.GetByIdAsync(employee.Id, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(employee);
            _unitOfWorkMock.Setup(uow => uow.Employees.DeleteAsync(employee.Id, It.IsAny<CancellationToken>()))
                           .Returns(Task.CompletedTask); // Setup for DeleteAsync
            // Act
            var result = await _employeeService.DeleteAsync(employee.Id, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.True);
                Assert.That(result.Message, Is.EqualTo("Employee deleted successfully"));
                Assert.That(result.Data, Is.True);
            });
            _unitOfWorkMock.Verify(uow => uow.Employees.DeleteAsync(employee.Id, It.IsAny<CancellationToken>()), Times.Once);
        }

                                                            //Negativni slucajevi
        [Test]
        public async Task GetByIdAsync_ShouldReturnNotFound_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var employeeId = Guid.NewGuid();

#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            _unitOfWorkMock.Setup(uow => uow.Employees.GetByIdAsync(employeeId, It.IsAny<CancellationToken>()))
                   .ReturnsAsync((Employee)null);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            // Act
            var result = await _employeeService.GetByIdAsync(employeeId, CancellationToken.None);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.Success, Is.False);
                Assert.That(result.Message, Is.EqualTo($"Employee with ID {employeeId} was not found."));
                Assert.That(result.Data, Is.Null);
            });
        }
        [Test]
public async Task AddAsync_ShouldReturnError_WhenEmployeeDtoIsNull()
{
            // Act
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var result = await _employeeService.AddAsync(null, CancellationToken.None);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Assert
            Assert.Multiple(() =>
    {
        Assert.That(result.Success, Is.False);
        Assert.That(result.Message, Is.EqualTo("Employee cannot be null."));
        Assert.That(result.Data, Is.Null);
    });
}
    }
}