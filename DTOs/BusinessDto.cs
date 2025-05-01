using SalonBooking.Models;

namespace SalonBooking.DTOs
{
    public class BusinessDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public BusinessType BusinessType { get; set; }
        public Service Service { get; set; }

    }
}
