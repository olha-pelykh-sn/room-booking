using Microsoft.EntityFrameworkCore;
using room_booking_backend.Data;
using room_booking_backend.Models;
using room_booking_backend.Repository.Abstraction;

namespace room_booking_backend.Repository.Implementation
{
    public class RoomRepository(AppDbContext dbContext) : IRoomRepository
    {
        public async Task<List<Room>> GetAvailableAsync(
            DateTime checkIn, DateTime checkOut, int capacity, CancellationToken cancellationToken)
        {
            return await dbContext.Rooms
                .Where(r =>
                    r.Capacity >= capacity &&
                    !r.Bookings.Any(b =>
                        b.CheckInDay < checkOut &&
                        b.CheckOutDay > checkIn))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Room> CreateAsync(Room room, CancellationToken cancellationToken)
        {
            await dbContext.Rooms.AddAsync(room, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return room;
        }

        public async Task<Room> UpdateAsync(int id, Room room, CancellationToken cancellationToken)
        {
            var existing = await dbContext.Rooms.FindAsync([id], cancellationToken);

            existing!.Name = room.Name;
            existing.Price = room.Price;
            existing.Capacity = room.Capacity;

            await dbContext.SaveChangesAsync(cancellationToken);
            return existing;
        }

        public async Task<Room> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var existing = await dbContext.Rooms.FindAsync([id], cancellationToken);

            dbContext.Rooms.Remove(existing!);
            await dbContext.SaveChangesAsync(cancellationToken);
            return existing!;
        }

        public async Task<Room?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Rooms.FindAsync([id], cancellationToken);
        }
        public async Task<bool> IsExistByNameAsync(string name, CancellationToken cancellationToken)
        {
            return await dbContext.Rooms
                .AsNoTracking()
                .AnyAsync(r => r.Name!.ToLower() == name!.ToLower(), cancellationToken);
        }
    }
}
