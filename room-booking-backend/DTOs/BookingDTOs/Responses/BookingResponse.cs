namespace room_booking_backend.DTOs.BookingDTOs.Responses
{
    /// <summary>Booking confirmation with cost breakdown.</summary>
    public class BookingResponse
    {
        /// <summary>Booking identifier.</summary>
        public int Id { get; set; }

        /// <summary>Room identifier.</summary>
        public int RoomId { get; set; }

        /// <summary>Room name.</summary>
        public string? RoomName { get; set; }

        /// <summary>Booking start date and time.</summary>
        public DateTime BookingStart { get; set; }

        /// <summary>Booking end date and time.</summary>
        public DateTime BookingEnd { get; set; }

        /// <summary>Booking duration in minutes.</summary>
        public int DurationMinutes { get; set; }

        /// <summary>Room rental cost (including the time-of-day tariff).</summary>
        public decimal RoomRentalCost { get; set; }

        /// <summary>Total cost of additional services.</summary>
        public decimal ServicesCost { get; set; }

        /// <summary>Grand total: rental cost + services cost.</summary>
        public decimal TotalPrice { get; set; }

        /// <summary>List of selected additional services.</summary>
        public List<ServiceInfo> Services { get; set; } = new List<ServiceInfo>();
    }

    /// <summary>Additional service details.</summary>
    public class ServiceInfo
    {
        /// <summary>Service identifier.</summary>
        public int Id { get; set; }

        /// <summary>Service name.</summary>
        public string? Name { get; set; }

        /// <summary>Service price.</summary>
        public decimal? Price { get; set; }
    }
}
