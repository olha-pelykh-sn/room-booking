using room_booking_backend.Models;

namespace room_booking_backend.Repository.Abstraction
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAvailableAsync(DateTime checkIn, DateTime checkOut, int capacity, CancellationToken cancellationToken);
        Task<Room> CreateAsync(Room room, CancellationToken cancellationToken);
        Task<Room?> UpdateAsync(int id, Room room, CancellationToken cancellationToken);
        Task<Room?> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken);
    }
}
