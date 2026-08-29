using AutoMapper;
using room_booking_backend.DTOs.RoomDTOs.Requests;
using room_booking_backend.DTOs.RoomDTOs.Responses;
using room_booking_backend.Models;
using room_booking_backend.Repository.Abstraction;
using room_booking_backend.Sevices.Interfaces;

namespace room_booking_backend.Sevices.Implementations
{
    public class RoomService(IRoomRepository roomRepository, IMapper mapper) : IRoomService
    {
        public async Task<List<DefaultRoomResponse>> GetAvailableAsync(
            GetAvailableRoomsRequest request, CancellationToken cancellationToken)
        {
            var rooms = await roomRepository.GetAvailableAsync(
                request.CheckIn, request.CheckOut, request.Capacity, cancellationToken);

            return mapper.Map<List<DefaultRoomResponse>>(rooms);
        }

        public async Task<DefaultRoomResponse?> CreateAsync(CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var roomName = request.Name ?? string.Empty;
            if (await roomRepository.IsExistByNameAsync(roomName, cancellationToken))
                throw new InvalidOperationException($"Room with name \"{roomName}\" already exists");

            var room = mapper.Map<Room>(request);
            var created = await roomRepository.CreateAsync(room, cancellationToken);

            return mapper.Map<DefaultRoomResponse>(created);
        }

        public async Task<DefaultRoomResponse?> UpdateAsync(int id, UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var existing = await roomRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Room with id {id} not found");

            var newName = request.Name ?? string.Empty;
            bool nameConflict = await roomRepository.IsExistByNameAsync(newName, cancellationToken);

            if (nameConflict && !string.Equals(existing.Name, newName, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Room with name \"{newName}\" already exists");

            var room = mapper.Map<Room>(request);
            var updated = await roomRepository.UpdateAsync(id, room, cancellationToken);

            return mapper.Map<DefaultRoomResponse>(updated);
        }

        public async Task<DefaultRoomResponse?> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await roomRepository.GetByIdAsync(id, cancellationToken)
                ?? throw new KeyNotFoundException($"Room with id {id} not found");

            var deleted = await roomRepository.DeleteAsync(id, cancellationToken);

            return mapper.Map<DefaultRoomResponse>(deleted);
        }
    }
}
