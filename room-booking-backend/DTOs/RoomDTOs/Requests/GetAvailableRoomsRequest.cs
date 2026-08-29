namespace room_booking_backend.DTOs.RoomDTOs.Requests
{
    /// <summary>Request to search for available rooms.</summary>
    public class GetAvailableRoomsRequest
    {
        /// <summary>Check-in date and time (UTC). Must be within working hours (06:00–23:00).</summary>
        /// <example>2026-09-10T10:00:00</example>
        public DateTime CheckIn { get; set; }

        /// <summary>Check-out date and time (UTC). Must be after CheckIn and within working hours.</summary>
        /// <example>2026-09-10T12:00:00</example>
        public DateTime CheckOut { get; set; }

        /// <summary>Required minimum room capacity (number of persons).</summary>
        /// <example>20</example>
        public int Capacity { get; set; }
    }
}
