using Microsoft.AspNetCore.Mvc;
using room_booking_backend.DTOs.BookingDTOs.Requests;
using room_booking_backend.DTOs.BookingDTOs.Responses;
using room_booking_backend.Sevices.Interfaces;

namespace room_booking_backend.Controllers
{
    /// <summary>
    /// Room booking with automatic cost calculation based on time of day.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class BookingsController(IBookingService bookingService) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateBookingRequest request, CancellationToken cancellationToken)
        {
            var booking = await bookingService.CreateAsync(request, cancellationToken);
            return Ok(booking);
        }
    }
}
