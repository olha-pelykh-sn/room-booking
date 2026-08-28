using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using room_booking_backend.Data;
using room_booking_backend.DTOs.RoomDTOs.Requests;
using room_booking_backend.Sevices.Interfaces;

namespace room_booking_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController(IRoomService roomService) : ControllerBase
    {

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable(
            [FromQuery] GetAvailableRoomsRequest request, CancellationToken cancellationToken)
        {
            var rooms = await roomService.GetAvailableAsync(request, cancellationToken);
            return Ok(rooms);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateRoomRequest request, CancellationToken cancellationToken)
        {
            var created = await roomService.CreateAsync(request, cancellationToken);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody]UpdateRoomRequest request, CancellationToken cancellationToken)
        {
            var updated = await roomService.UpdateAsync(id, request, cancellationToken);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await roomService.DeleteAsync(id, cancellationToken);
            return Ok(deleted);
        }
    }
}
