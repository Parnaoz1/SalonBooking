
namespace SalonBooking.Models
{
    public class Appointment
    {
       
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public int WorkerId { get; set; }
        public int Duration { get; set; } // Duration in minutes
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime Date { get; set; }
        public AppointmentStatus Status { get; set; }
    }

    public enum AppointmentStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }
}
