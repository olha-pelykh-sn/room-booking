using Microsoft.EntityFrameworkCore;
using room_booking_backend.Data;
using room_booking_backend.Models;
using room_booking_backend.Repository.Abstraction;

namespace room_booking_backend.Repository.Implementation
{
    public class BookingRepository(AppDbContext dbContext) : IBookingRepository
    {
        public async Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken)
        {
            await dbContext.Bookings.AddAsync(booking, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return booking;
        }

        public async Task<Booking?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await dbContext.Bookings
                .Include(b => b.Room)
                .Include(b => b.Services)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<List<Service>> GetServicesByIdsAsync(List<int> ids, CancellationToken cancellationToken)
        {
            return await dbContext.Services
                .Where(s => ids.Contains(s.Id))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsRoomAvailableAsync(
            int roomId, DateTime start, DateTime end, CancellationToken cancellationToken)
        {
            return !await dbContext.Bookings
                .AnyAsync(b =>
                    b.RoomId == roomId &&
                    b.CheckInDay < end &&
                    b.CheckOutDay > start,
                    cancellationToken);
        }
    }
}
