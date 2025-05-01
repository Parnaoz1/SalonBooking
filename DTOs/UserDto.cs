using SalonBooking.Models;

namespace SalonBooking.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; }  
    }
}
