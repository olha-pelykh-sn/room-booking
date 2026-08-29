namespace room_booking_backend.Constants
{
    public static class BusinessRules
    {
        public static readonly TimeSpan WorkingHoursOpen  = TimeSpan.FromHours(6);
        public static readonly TimeSpan WorkingHoursClose = TimeSpan.FromHours(23);

        public static int MaxBookingDurationMinutes =>
            (int)(WorkingHoursClose - WorkingHoursOpen).TotalMinutes; // 1020

        public const int MaxRoomCapacity = 1000;
        public const int MaxRoomNameLength = 100;
    }
}
