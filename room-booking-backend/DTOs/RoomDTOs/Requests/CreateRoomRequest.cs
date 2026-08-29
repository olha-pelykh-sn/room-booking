namespace room_booking_backend.DTOs.RoomDTOs.Requests
{
    /// <summary>Request to create a new room.</summary>
    public class CreateRoomRequest
    {
        /// <summary>Unique room name (maximum 100 characters).</summary>
        /// <example>Conference Room A</example>
        public string? Name { get; set; }

        /// <summary>Base hourly rental rate.</summary>
        /// <example>500.00</example>
        public decimal? Price { get; set; }

        /// <summary>Maximum room capacity in persons (1–1000).</summary>
        /// <example>30</example>
        public int? Capacity { get; set; }
    }
}
