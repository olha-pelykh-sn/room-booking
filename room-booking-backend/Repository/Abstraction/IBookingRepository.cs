using room_booking_backend.Models;

namespace room_booking_backend.Repository.Abstraction
{
    public interface IBookingRepository
    {
        Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken);
        Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<List<Service>> GetServicesByIdsAsync(List<int> ids, CancellationToken cancellationToken);
        Task<bool> IsRoomAvailableAsync(int roomId, DateTime start, DateTime end, CancellationToken cancellationToken);
    }
}
