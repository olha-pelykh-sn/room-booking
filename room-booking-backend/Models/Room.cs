namespace room_booking_backend.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Capacity { get; set; }

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
