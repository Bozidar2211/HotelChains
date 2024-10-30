using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotel
    {
        [Key]
        public Guid id { get; set; }
        public required string Name { get; set; }
        public int YearOpened { get; set; }
        public int NumberOfEmloyees { get; set; }
        public int NumberOfRooms { get; set; }
        public Guid HotelChainId { get; set; }
        public required HotelChain HotelChain { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
