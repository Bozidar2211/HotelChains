using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class HotelChainDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public int YearEstablished { get; set; }
        public required List<HotelDto> Hotels { get; set; }
    }

}
