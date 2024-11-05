using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Hotel
    {
        [Key]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int YearOpened { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfRooms { get; set; }
        public Guid HotelChainId { get; set; }
        public required HotelChain HotelChain { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
