namespace SalonBooking.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int DurationInMinutes { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }

        // Add this property to fix the CS1061 error
        public ICollection<WorkerServiceRelation> WorkerServices { get; set; }
    }
}
