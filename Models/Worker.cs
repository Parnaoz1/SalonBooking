using System.ComponentModel.DataAnnotations;

namespace SalonBooking.Models
{
    public class Worker
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        // კავშირი Business-სთან
        public int BusinessId { get; set; }
        public Business Business { get; set; }

        // თანამშრომლის სამუშაო საათები
        public ICollection<WorkingHours> WorkingHours { get; set; }

        // დაჯავშნები
        public ICollection<Appointment> Appointments { get; set; }

        // სერვისები რაც ამ თანამშრომელს შეუძლია გააკეთოს
        public ICollection<WorkerServiceRelation> WorkerServices { get; set; }
    }
}
