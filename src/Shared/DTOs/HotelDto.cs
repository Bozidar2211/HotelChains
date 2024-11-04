using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HotelDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int YearOpened { get; set; }
        public int NumberOfEmployees { get; set; }
        public int NumberOfRooms { get; set; }
        public Guid HotelChainId { get; set; }
    }
}
