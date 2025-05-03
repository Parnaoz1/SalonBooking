using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SalonBooking.Models
{
    public class WorkingHours
    {
        public int Id { get; set; }
        public DayOfWeek Day {  get; set; }
        public TimeSpan WorkStart { get; set; }
        public TimeSpan WorkEnd { get; set; }
        public DayOfWeek DayOfWeek { get; set; }

        public int WorkerId { get; set; }
        public Worker Worker { get; set; }

        public int SalonId { get; set; }
        public Business Business { get; set; }
    }
}
