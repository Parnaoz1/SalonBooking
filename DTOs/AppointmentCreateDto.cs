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
        public int ServiceId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int ServiceDuration { get; set; } // in minutes
    }
}
