using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
