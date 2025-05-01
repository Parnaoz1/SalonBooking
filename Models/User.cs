using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SalonBooking.Models
{
    public class User : IdentityUser
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public ICollection<Review> Reviews { get; set; }

        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Business> OwnedBusiness { get; set; }
    }

    public enum UserRole
    {
        Client,
        SalonOwner,
        Admin
    }
}
