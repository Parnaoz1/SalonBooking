using Microsoft.EntityFrameworkCore;
using SalonBooking.Common;
using SalonBooking.Data;
using SalonBooking.DTOs;
using SalonBooking.Models;
using SalonBooking.Services.Interfaces;

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
            var appointment = new Appointment
            {
                UserId = dto.UserId,
                WorkerId = dto.WorkerId,
                ServiceId = dto.ServiceId,
                StartTime = dto.StartTime,
                EndTime = dto.StartTime.AddMinutes(dto.ServiceDuration),
            };

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
                .Select(a => new AppointmentDto
                {
                    Id = a.Id,
                    StartTime = a.StartTime,
                    EndTime = a.EndTime,
                    WorkerId = a.WorkerId,
                    UserId = a.UserId
                })
                .ToListAsync();

            return appointments;
        }

        public async Task<AppointmentDto> GetAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment == null)
                return null;

            return new AppointmentDto
            {
                Id = appointment.Id,
                StartTime = appointment.StartTime,
                EndTime = appointment.EndTime,
                WorkerId = appointment.WorkerId,
                UserId = appointment.UserId
            };
            
        }

        public async Task<List<TimeSpan>> GetAvailableTimeSlots(string userId, DateTime date, int serviceDuration)
        {
            // 1. Get working hours for that day
            var workingHours = await _context.WorkingHours
                .FirstOrDefaultAsync(w => w.SalonId == int.Parse(userId) && w.Day == date.DayOfWeek);

            if(workingHours == null)
            {
                return new List<TimeSpan>(); // No working hours for that day
            }

            // 2. Get existing appointments for that day
            var appointments = await _context.Appointments
                .Where(a => a.BusinessId == int.Parse(userId) && a.StartTime.Date == date.Date)
                .ToListAsync();

            var start = workingHours.OpenTime;
            var end = workingHours.CloseTime;
            var interval = TimeSpan.FromMinutes(serviceDuration);

            var availableSlots = new List<TimeSpan>();
            for (var time = start; time <= end - interval; time += interval)
            {
                var slotStart = date.Date + time;
                var slotEnd = slotStart + interval;

                bool isBooked = appointments.Any(a =>
                    (a.StartTime < slotEnd && a.EndTime > slotStart) || // Overlapping appointments
                    (a.StartTime == slotStart && a.EndTime == slotEnd)); // Exact match

                if (!isBooked)
                    availableSlots.Add(time);
            }

            return availableSlots;

        }
    }
}
