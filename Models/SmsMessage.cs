namespace SalonBooking.Models
{
    public class SmsMessage
    {
        public int Id { get; set; }
        public string ToPhoneNumber { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsDelivered { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }

}
