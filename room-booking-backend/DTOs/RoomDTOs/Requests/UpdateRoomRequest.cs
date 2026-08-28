namespace room_booking_backend.DTOs.RoomDTOs.Requests
{
    public class UpdateRoomRequest
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Capacity { get; set; }
    }
}
