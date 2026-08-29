using Microsoft.AspNetCore.Mvc;
using room_booking_backend.DTOs.RoomDTOs.Requests;
using room_booking_backend.DTOs.RoomDTOs.Responses;
using room_booking_backend.Sevices.Interfaces;

namespace room_booking_backend.Controllers
{
    /// <summary>
    /// Room management: search for available rooms, create, update and delete.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RoomsController(IRoomService roomService) : ControllerBase
    {
        /// <summary>Get a list of available rooms matching the search criteria.</summary>
        /// <param name="request">Search parameters: check-in date, check-out date, required capacity.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>List of rooms available for the specified time slot.</returns>
        [HttpGet("available")]
        [ProducesResponseType(typeof(List<DefaultRoomResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAvailable(
            [FromQuery] GetAvailableRoomsRequest request, CancellationToken cancellationToken)
        {
            var rooms = await roomService.GetAvailableAsync(request, cancellationToken);
            return Ok(rooms);
        }

        /// <summary>Create a new room.</summary>
        /// <param name="request">Room data: name, hourly price, capacity.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>The created room with its assigned ID.</returns>
        /// <remarks>Room name must be unique. Returns 409 Conflict if a room with the same name already exists.</remarks>
        [HttpPost]
        [ProducesResponseType(typeof(DefaultRoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create(
            [FromBody] CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var created = await roomService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }

        /// <summary>Update an existing room.</summary>
        /// <param name="id">Room identifier.</param>
        /// <param name="request">Updated room data.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>The updated room.</returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DefaultRoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update(
            int id, [FromBody] UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var updated = await roomService.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        /// <summary>Delete a room by identifier.</summary>
        /// <param name="id">Room identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>The deleted room.</returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DefaultRoomResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await roomService.DeleteAsync(id, cancellationToken);
            return Ok(deleted);
        }
    }
}
