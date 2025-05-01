using SalonBooking.Common;
using SalonBooking.DTOs;

namespace SalonBooking.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<TimeSpan>> GetAvailableTimeSlots(string userId, DateTime date, int serviceDuration);
        Task<List<AppointmentDto>> GetAppointmentsForUserAsync(string userId);
        Task<AppointmentDto> GetAppointmentAsync(int appointmentId);
        Task<Result<AppointmentDto>> CreateAppointmentAsync(AppointmentCreateDto dto);
    }
}
