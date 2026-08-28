using room_booking_backend.DTOs.RoomDTOs.Requests;
using room_booking_backend.DTOs.RoomDTOs.Responses;

namespace room_booking_backend.Sevices.Interfaces
{
    public interface IRoomService 
    {
        Task<List<DefaultRoomResponse>> GetAvailableAsync(GetAvailableRoomsRequest request, CancellationToken cancellationToken);
        Task<DefaultRoomResponse?> CreateAsync(CreateRoomRequest request, CancellationToken cancellationToken);
        Task<DefaultRoomResponse?> UpdateAsync(int id, UpdateRoomRequest request, CancellationToken cancellationToken);
        Task<DefaultRoomResponse?> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}
