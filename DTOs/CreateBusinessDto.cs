using SalonBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class CreateBusinessDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public BusinessType BusinessType { get; set; }

        [Required]
        public SubscriptionPlan SubscriptionPlan { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public ICollection<Service> Services { get; set; }
    }
}
