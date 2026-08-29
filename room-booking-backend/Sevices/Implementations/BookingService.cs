using AutoMapper;
using room_booking_backend.Constants;
using room_booking_backend.DTOs.BookingDTOs.Requests;
using room_booking_backend.DTOs.BookingDTOs.Responses;
using room_booking_backend.Models;
using room_booking_backend.Repository.Abstraction;
using room_booking_backend.Sevices.Interfaces;

namespace room_booking_backend.Sevices.Implementations
{
    public class BookingService(
        IBookingRepository bookingRepository,
        IRoomRepository roomRepository,
        IMapper mapper) : IBookingService
    {
        public async Task<BookingResponse> CreateAsync(
            CreateBookingRequest request, CancellationToken cancellationToken)
        {
            // Validate room exists
            var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken)
                ?? throw new KeyNotFoundException($"Room with id {request.RoomId} not found");

            // Calculate booking end time
            var bookingEnd = request.BookingStart.AddMinutes(request.DurationMinutes);

            var openTime  = TimeOnly.FromTimeSpan(BusinessRules.WorkingHoursOpen);
            var closeTime = TimeOnly.FromTimeSpan(BusinessRules.WorkingHoursClose);
            var startTime = TimeOnly.FromDateTime(request.BookingStart);
            var endTime = TimeOnly.FromDateTime(bookingEnd);

            if (startTime < openTime || endTime > closeTime || bookingEnd.Date != request.BookingStart.Date)
                throw new InvalidOperationException(
                    $"Bookings are only allowed between {openTime} and {closeTime}. " +
                    $"Your booking would run from {startTime} to {endTime}.");

            // Check room availability
            bool isAvailable = await bookingRepository.IsRoomAvailableAsync(
                request.RoomId, request.BookingStart, bookingEnd, cancellationToken);

            if (!isAvailable)
                throw new InvalidOperationException(
                    $"Room \"{room.Name}\" is not available for the selected time slot.");

            // Resolve services
            var services = request.ServiceIds.Any()
                ? await bookingRepository.GetServicesByIdsAsync(request.ServiceIds, cancellationToken)
                : new List<Service>();

            // Calculate rental cost with time-based pricing
            decimal rentalCost = CalculateRentalCost(room.Price ?? 0, request.BookingStart, request.DurationMinutes);

            // Calculate services cost
            decimal servicesCost = services.Sum(s => s.Price ?? 0);

            decimal totalPrice = rentalCost + servicesCost;

            var booking = new Booking
            {
                RoomId = request.RoomId,
                CheckInDay = request.BookingStart,
                CheckOutDay = bookingEnd,
                TotalPrice = totalPrice,
                Services = services
            };

            var created = await bookingRepository.CreateAsync(booking, cancellationToken);

            return new BookingResponse
            {
                Id = created.Id,
                RoomId = room.Id,
                RoomName = room.Name,
                BookingStart = request.BookingStart,
                BookingEnd = bookingEnd,
                DurationMinutes = request.DurationMinutes,
                RoomRentalCost = rentalCost,
                ServicesCost = servicesCost,
                TotalPrice = totalPrice,
                Services = mapper.Map<List<ServiceInfo>>(services)
            };
        }

        /// <summary>
        /// Calculates the room rental cost based on time-of-day tariff zones:
        /// - Morning  (06:00–09:00): -10% discount
        /// - Standard (09:00–12:00 and 14:00–18:00): base price
        /// - Peak     (12:00–14:00): +15% surcharge
        /// - Evening  (18:00–23:00): -20% discount
        /// When a booking spans multiple zones, the cost is split proportionally.
        /// </summary>
        private static decimal CalculateRentalCost(decimal baseHourlyPrice, DateTime start, int durationMinutes)
        {
            var tariffZones = new List<(double zoneStart, double zoneEnd, decimal multiplier)>
            {
                (6,  9,  0.90m), // Morning   -10%
                (9,  12, 1.00m), // Standard
                (12, 14, 1.15m), // Peak      +15%
                (14, 18, 1.00m), // Standard
                (18, 23, 0.80m), // Evening   -20%
            };

            decimal totalCost = 0m;
            double remainingMinutes = durationMinutes;
            double currentHour = start.Hour + start.Minute / 60.0;

            while (remainingMinutes > 0)
            {
                var zone = tariffZones.FirstOrDefault(z => currentHour >= z.zoneStart && currentHour < z.zoneEnd);

                double minutesInZone;
                decimal multiplier;

                if (zone == default)
                {
                    multiplier = 1.00m;
                    minutesInZone = remainingMinutes;
                }
                else
                {
                    double minutesUntilZoneEnd = (zone.zoneEnd - currentHour) * 60.0;
                    minutesInZone = Math.Min(remainingMinutes, minutesUntilZoneEnd);
                    multiplier = zone.multiplier;
                }

                totalCost += baseHourlyPrice * multiplier * (decimal)(minutesInZone / 60.0);

                currentHour += minutesInZone / 60.0;
                remainingMinutes -= minutesInZone;
            }

            return Math.Round(totalCost, 2);
        }
    }
}
