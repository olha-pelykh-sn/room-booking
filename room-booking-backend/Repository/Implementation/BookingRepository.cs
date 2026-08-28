using room_booking_backend.Data;
using room_booking_backend.Models;
using room_booking_backend.Repository.Abstraction;

namespace room_booking_backend.Repository.Implementation
{
    public class BookingRepository(AppDbContext dbContext)
    {
        public Task<Booking> CreateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
