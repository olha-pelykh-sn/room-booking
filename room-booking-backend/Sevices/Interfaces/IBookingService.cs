using room_booking_backend.DTOs.BookingDTOs.Requests;
using room_booking_backend.DTOs.BookingDTOs.Responses;

namespace room_booking_backend.Sevices.Interfaces
{
    public interface IBookingService
    {
        Task<BookingResponse> CreateAsync(CreateBookingRequest request, CancellationToken cancellationToken);
    }
}
