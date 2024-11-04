namespace Shared.DTOs
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Position { get; set; }
        public Guid HotelId { get; set; }
    }

}
