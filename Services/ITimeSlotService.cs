using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalonBooking.Services.Interfaces
{
    public interface ITimeSlotService
    {
        /// <summary>
        /// აბრუნებს ემისამუშაო დროის სლოტებს worker-ისთვის მოცემულ დღეს და სერვისის ხანგრძლივობაზე დაყრდნობით.
        /// </summary>
        /// <param name="workerId">თანამშრომლის Id</param>
        /// <param name="date">დღის თარიღი (Date part ჩათვლით)</param>
        /// <param name="serviceDuration">სერვისის ხანგრძლივობა წუთებში</param>

        Task<List<TimeSpan>> GetAvailableTimeSlotsAsync(int workerId, DateTime date, int serviceDuration);
    }
}
