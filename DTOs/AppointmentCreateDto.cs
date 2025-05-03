using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class AppointmentCreateDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public List<int> ServiceIds { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int BusinessId { get; set; } // Optional, if you want to specify a business
        [Required]
        public int ServiceDuration { get; set; } // in minutes
    }
}
