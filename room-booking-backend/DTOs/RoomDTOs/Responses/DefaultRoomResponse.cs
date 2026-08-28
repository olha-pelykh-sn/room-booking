namespace room_booking_backend.DTOs.RoomDTOs.Responses
{
    public class DefaultRoomResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Capacity { get; set; }
    }
}
