using FluentValidation;
using room_booking_backend.Constants;
using room_booking_backend.DTOs.BookingDTOs.Requests;

namespace room_booking_backend.Validations
{
    public class CreateBookingRequestValidator : AbstractValidator<CreateBookingRequest>
    {
        public CreateBookingRequestValidator()
        {
            RuleFor(x => x.RoomId)
                .GreaterThan(0).WithMessage("RoomId must be greater than 0");

            RuleFor(x => x.BookingStart)
                .NotEmpty().WithMessage("Booking start date and time is required")
                .Must(dt => dt > DateTime.UtcNow).WithMessage("Booking start must be in the future");

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 minutes")
                .LessThanOrEqualTo(BusinessRules.MaxBookingDurationMinutes)
                .WithMessage($"Duration must not exceed {BusinessRules.MaxBookingDurationMinutes} minutes (17 hours)");

            RuleFor(x => x.ServiceIds)
                .NotNull().WithMessage("ServiceIds list must not be null");
        }
    }
}
