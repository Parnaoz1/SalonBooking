using Microsoft.EntityFrameworkCore;
using SalonBooking.Data;
using SalonBooking.Services.Interfaces;
using System.Linq;

namespace SalonBooking.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly AppDbContext _context;

        public TimeSlotService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TimeSpan>> GetAvailableTimeSlotsAsync(int workerId, DateTime date, int serviceDuration)
        {
            // 1) ამოვიღოთ სამუშაო საათები
            var wh = await _context.WorkingHours
                .FirstOrDefaultAsync(x => x.WorkerId == workerId
                                        && x.Day == date.DayOfWeek);

            if (wh == null)
                return new List<TimeSpan>();

            var start = wh.OpenTime;
            var end = wh.CloseTime;

            // 2) ყველა პოტენციური სლოტი 15-წუთიანი ინტერვალით
            var slots = new List<TimeSpan>();
            for (var t = start; t + TimeSpan.FromMinutes(serviceDuration) <= end; t = t.Add(TimeSpan.FromMinutes(15)))
            {
                slots.Add(t);
            }

            // 3) გამოვიტანოთ უკვე დაჯავშნილი appointment-ები
            var booked = await _context.Appointments
                .Where(a => a.WorkerId == workerId
                    && a.Date.Date == date.Date)
                .Select(a => new
                {
                    Start = a.StartTime.TimeOfDay,
                    End = a.EndTime.TimeOfDay
                })
                .ToListAsync();

            // 4) ვამოწმებთ, რომ სლოტი არ იყოს დაჯავშნილი
            var available = slots
                .Where(slot => !booked.Any(b =>
                    slot < b.End
                    && slot + TimeSpan.FromMinutes(serviceDuration) > b.Start))
                .ToList();

            return available;
        }
    }
}
