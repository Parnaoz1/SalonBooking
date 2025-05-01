namespace SalonBooking.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public int BusinessTypeId { get; set; }
        public BusinessType BusinessType { get; set; }

        public string OwnerId { get; set; }
        public User Owner { get; set; }

        public int SubscriptionPlanId { get; set; }
        public SubscriptionPlan SubscriptionPlan { get; set; }

        public ICollection<Service> Services { get; set; }
        public ICollection<WorkingHours> WorkingHours { get; set; } 
    }
}
