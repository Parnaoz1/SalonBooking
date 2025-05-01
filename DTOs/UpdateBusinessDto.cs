using System.Collections.Generic; // Ensure this is included
using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class UpdateBusinessDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }

        public int BusinessTypeId { get; set; }

        public int SubscriptionPlanId { get; set; }

        public ICollection<WorkingHoursDto> WorkingHours { get; set; } = new List<WorkingHoursDto>();
    }
}
