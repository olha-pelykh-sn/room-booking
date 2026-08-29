namespace room_booking_backend.DTOs.BookingDTOs.Requests
{
    /// <summary>Request to create a room booking.</summary>
    public class CreateBookingRequest
    {
        /// <summary>Room identifier.</summary>
        /// <example>1</example>
        public int RoomId { get; set; }

        /// <summary>Booking start date and time (UTC). The venue operates from 06:00 to 23:00.</summary>
        /// <example>2026-09-10T10:00:00</example>
        public DateTime BookingStart { get; set; }

        /// <summary>Booking duration in minutes (1–1020).</summary>
        /// <example>120</example>
        public int DurationMinutes { get; set; }

        /// <summary>List of selected additional service IDs. Pass an empty array if no services are needed.</summary>
        /// <example>[1, 2]</example>
        public List<int> ServiceIds { get; set; } = new List<int>();
    }
}
