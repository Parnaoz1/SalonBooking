using SalonBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class UserRegisterDto    
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }    

        public UserRole Role { get; set; } = UserRole.Customer;
    }
}

