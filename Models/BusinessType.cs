namespace SalonBooking.Models
{
    public class BusinessType
    {
        public int Id { get; set; }
        public BusinessName Name { get; set; }
    }

    public enum BusinessName 
    {
        Salon,
        Dentist
    }
    

}
