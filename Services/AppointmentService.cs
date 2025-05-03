using Microsoft.EntityFrameworkCore;
using SalonBooking.Common;
using SalonBooking.Data;
using SalonBooking.DTOs;
using SalonBooking.Models;
using SalonBooking.Services.Interfaces;
using System.Web.Mvc;

namespace SalonBooking.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly AppDbContext _context;

        public AppointmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<AppointmentDto>> CreateAppointmentAsync(AppointmentCreateDto dto)
        {
            var serviceIds = dto.ServiceIds;
            var services = await _context.Services
                            .Where(s => serviceIds.Contains(s.Id))
                            .ToListAsync();

            int totalDuration = services.Sum(s => s.DurationInMinutes);

            var appointment = new Appointment
            {
                StartTime = dto.StartTime,
                EndTime = dto.StartTime.AddMinutes(totalDuration),  // Set end time based on total duration
                UserId = dto.UserId,
                WorkerId = dto.WorkerId,
                BusinessId = dto.BusinessId,
                // You can add any other necessary fields here
            };

            appointment.Duration = totalDuration;
            appointment.EndTime = dto.StartTime.AddMinutes(totalDuration);
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var result = new AppointmentDto
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                WorkerId = appointment.WorkerId,
                UserId = appointment.UserId
            };

            return Result<AppointmentDto>.Success(result);
        }
        public async Task<List<AppointmentDto>> GetAppointmentsForUserAsync(string userId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return appointments.Select(a => new AppointmentDto
            {
                Id = a.Id,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                UserId = a.UserId,
                WorkerId = a.WorkerId,
                ServiceId = a.ServiceId.ToString() // Convert int to string to match the AppointmentDto property type
            }).ToList();
        }

        public async Task<AppointmentDto> GetAppointmentAsync(int id)
        {
            var a = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (a == null) return null;

            return new AppointmentDto
            {
                Id = a.Id,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                UserId = a.UserId,
                WorkerId = a.WorkerId,
                ServiceId = a.ServiceId.ToString(), // Convert int to string to match the AppointmentDto property type
            };
        }

        public async Task<List<TimeSpan>> GetAvailableTimeSlots(string workerId, DateTime date, int duration)
        {
            if (!int.TryParse(workerId, out int workerIdInt))
                return new List<TimeSpan>(); // invalid workerId format

            var worker = await _context.Workers
                .Include(w => w.WorkingHours)
                .FirstOrDefaultAsync(w => w.Id == workerIdInt);

            if (worker == null) return new List<TimeSpan>();

            var dayOfWeek = date.DayOfWeek;

            var workingHours = worker.WorkingHours.FirstOrDefault(wh => wh.DayOfWeek == dayOfWeek);
            if (workingHours == null) return new List<TimeSpan>(); // worker doesn't work on this day

            var start = workingHours.WorkStart;
            var end = workingHours.WorkEnd;

            var appointments = await _context.Appointments
                .Where(a => a.WorkerId == workerIdInt && a.StartTime.Date == date.Date)
                .ToListAsync();

            var availableSlots = new List<TimeSpan>();

            for (var time = start; time.Add(TimeSpan.FromMinutes(duration)) <= end; time = time.Add(TimeSpan.FromMinutes(15)))
            {
                var slotStart = date.Date + time;
                var slotEnd = slotStart.AddMinutes(duration);

                bool overlaps = appointments.Any(a => slotStart < a.EndTime && slotEnd > a.StartTime);
                if (!overlaps)
                    availableSlots.Add(time);
            }

            return availableSlots;
        }

        private Result<AppointmentDto> BadRequest(ModelState modelState)
        {
            throw new NotImplementedException();
        }
    }
}
