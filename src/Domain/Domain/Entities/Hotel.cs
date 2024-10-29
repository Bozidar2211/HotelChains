using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Hotel
    {
        public Guid id { get; set; }
        public required string Name { get; set; }
        public int YearOpened { get; set; }
        public int NumberOfEmloyees { get; set; }
        public int NumberOfRooms { get; set; }
        public int HotelChainId { get; set; }
        public required HotelChain HotelChain { get; set; }
    }
}
