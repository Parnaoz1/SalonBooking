namespace SalonBooking.Models
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerMonth { get; set; }
        public string Description { get; set; }

        public ICollection<Business> Businesses { get; set; }
    }
}
