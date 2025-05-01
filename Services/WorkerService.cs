using SalonBooking.Data;
using SalonBooking.DTOs;
using SalonBooking.Models;

using Microsoft.EntityFrameworkCore;
using SalonBooking.Services.Interfaces;

namespace SalonBooking.Services
{
    public class WorkerService : IWorkerService
    {
        public readonly AppDbContext _context;

        public WorkerService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<WorkerDto>> GetAvailableWorkers(DateTime date, int serviceDuration)
        {
            var workers = await _context.Workers
                 .Include(w => w.Appointments)
                 .Include(w => w.WorkingHours)
                 .ToListAsync();

            var availableWorkers = new List<WorkerDto>();

            foreach (var worker in workers)
            {
                var WorkingHours = worker.WorkingHours
                    .FirstOrDefault(wh => wh.Day == date.DayOfWeek);

                if (WorkingHours == null)
                    continue;

                var start = new DateTime(date.Year, date.Month, date.Day, WorkingHours.OpenTime.Hours, WorkingHours.OpenTime.Minutes, 0);
                var end = new DateTime(date.Year, date.Month, date.Day, WorkingHours.CloseTime.Hours, WorkingHours.CloseTime.Minutes, 0);

                bool isAvailable = true;

                foreach (var appointment in worker.Appointments)
                {
                    var appointmentStart = appointment.StartTime;
                    var appointmentEnd = appointment.StartTime.AddMinutes(appointment.Duration);

                    if (date < appointmentEnd && date.AddMinutes(serviceDuration) > appointmentStart)
                    {
                        isAvailable = false;
                        break;
                    }
                }

                if (isAvailable && date >= start && date.AddMinutes(serviceDuration) <= end)
                {
                    availableWorkers.Add(new WorkerDto
                    {
                        Id = worker.Id,
                        FullName = worker.FullName,
                        PhoneNumber = worker.PhoneNumber
                    });
                }
            }

            return availableWorkers;
        }

        // სერვისის მიბმა თანამშრომელზე
        public async Task AssignServiceToWorker(int workerId, int serviceId)
        {
            var relation = new WorkerServiceRelation
            {
                WorkerId = workerId,
                ServiceId = serviceId
            };

            _context.Set<WorkerServiceRelation>().Add(relation);
            await _context.SaveChangesAsync();
        }
    }
}
