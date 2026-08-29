namespace room_booking_backend.DTOs.RoomDTOs.Requests
{
    /// <summary>Request to update an existing room.</summary>
    public class UpdateRoomRequest
    {
        /// <summary>New unique room name (maximum 100 characters).</summary>
        /// <example>Conference Room B</example>
        public string? Name { get; set; }

        /// <summary>New base hourly rental rate.</summary>
        /// <example>600.00</example>
        public decimal? Price { get; set; }

        /// <summary>New maximum room capacity in persons (1–1000).</summary>
        /// <example>50</example>
        public int? Capacity { get; set; }
    }
}
