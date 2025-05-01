using SalonBooking.Models;
using System.ComponentModel.DataAnnotations;

namespace SalonBooking.DTOs
{
    public class UserUpdateDto
    {
        public string Name { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } 

        public UserRole? Role { get; set; }
    }
}
