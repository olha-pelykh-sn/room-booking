namespace room_booking_backend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public decimal? TotalPrice { get; set; }
        public DateTime? CheckInDay { get; set; }
        public DateTime? CheckOutDay { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        public ICollection<Service> Services { get; set; } = new List<Service>();
    }
}
