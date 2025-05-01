using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonBooking.DTOs;
using SalonBooking.Models;
using SalonBooking.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace SalonBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ITimeSlotService _timeSlotService;

        public AppointmentController(IAppointmentService appointmentService, ITimeSlotService timeSlotService)
        {
            _appointmentService = appointmentService;
            _timeSlotService = timeSlotService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _appointmentService.CreateAppointmentAsync(dto);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpGet("user/{userID}")]
        public async Task<IActionResult> GetUserAppointments(string userID)
        {
            var appointments = await _appointmentService.GetAppointmentsForUserAsync(userID);
            return Ok(appointments);
        }

        [HttpGet("available-times")]
        public async Task<IActionResult> GetAvailableTimeSlots([FromBody] TimeSlotRequestDto req)
        {
            var slots = await _timeSlotService
                .GetAvailableTimeSlotsAsync(req.WorkerId, req.Date, req.ServiceDuration);

            var formatted = slots.Select((TimeSpan t) => t.ToString(@"hh\mm"));
            return Ok(formatted);
        }
    }
}
