using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public Guid HotelId { get; set; }
        public required Hotel Hotel { get; set; }
        public required string Position { get; set; }

    }
}
