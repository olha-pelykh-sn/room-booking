using FluentValidation;
using room_booking_backend.Constants;
using room_booking_backend.DTOs.RoomDTOs.Requests;

namespace room_booking_backend.Validations
{
    public class GetAvailableRoomsRequestValidator : AbstractValidator<GetAvailableRoomsRequest>
    {
        public GetAvailableRoomsRequestValidator()
        {
            RuleFor(x => x.CheckIn)
                .NotEmpty().WithMessage("CheckIn date is required")
                .Must(dt => dt > DateTime.UtcNow).WithMessage("CheckIn must be in the future")
                .Must(IsWithinWorkingHours)
                .WithMessage($"CheckIn time must be between {BusinessRules.WorkingHoursOpen:hh\\:mm} and {BusinessRules.WorkingHoursClose:hh\\:mm}");

            RuleFor(x => x.CheckOut)
                .NotEmpty().WithMessage("CheckOut date is required")
                .GreaterThan(x => x.CheckIn).WithMessage("CheckOut must be later than CheckIn")
                .Must(IsWithinWorkingHours)
                .WithMessage($"CheckOut time must be between {BusinessRules.WorkingHoursOpen:hh\\:mm} and {BusinessRules.WorkingHoursClose:hh\\:mm}");

            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be greater than 0")
                .LessThanOrEqualTo(BusinessRules.MaxRoomCapacity)
                .WithMessage($"Capacity must not exceed {BusinessRules.MaxRoomCapacity}");
        }

        private static bool IsWithinWorkingHours(DateTime dateTime)
        {
            var time = dateTime.TimeOfDay;
            return time >= BusinessRules.WorkingHoursOpen && time <= BusinessRules.WorkingHoursClose;
        }
    }
}