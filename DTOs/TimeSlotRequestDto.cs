using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class TimeSlotRequestDto
    {
        [Key]
        [Required]
        public int WorkerId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int ServiceDuration { get; set; }
    }
}
