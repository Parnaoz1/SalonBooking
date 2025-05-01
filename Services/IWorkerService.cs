using SalonBooking.DTOs;

namespace SalonBooking.Services.Interfaces
{
    public interface IWorkerService
    {
        Task<List<WorkerDto>> GetAvailableWorkers(DateTime date, int serviceDuration);

        Task AssignServiceToWorker(int workerId, int serviceId);
    }
}
