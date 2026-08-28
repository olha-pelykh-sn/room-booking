namespace room_booking_backend.DTOs.RoomDTOs.Requests
{
    public class GetAvailableRoomsRequest
    {
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int Capacity { get; set; }
    }
}
