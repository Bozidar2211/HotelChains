using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class HotelChain
    {
        [Key]
        public Guid Id { get; set; }
        public int YearEstablished { get; set; }
        public required ICollection<Hotel> Hotels { get; set; }
    }
}
