namespace room_booking_backend.DTOs.RoomDTOs.Responses
{
    /// <summary>Room details.</summary>
    public class DefaultRoomResponse
    {
        /// <summary>Room identifier.</summary>
        public int Id { get; set; }

        /// <summary>Room name.</summary>
        public string? Name { get; set; }

        /// <summary>Base hourly rental rate.</summary>
        public decimal? Price { get; set; }

        /// <summary>Maximum room capacity in persons.</summary>
        public int? Capacity { get; set; }
    }
}
