using FluentValidation;
using room_booking_backend.Constants;
using room_booking_backend.DTOs.RoomDTOs.Requests;

namespace room_booking_backend.Validations
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(BusinessRules.MaxRoomNameLength)
                .WithMessage($"Name must not exceed {BusinessRules.MaxRoomNameLength} characters");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Capacity)
                .NotNull().WithMessage("Capacity is required")
                .GreaterThan(0).WithMessage("Capacity must be greater than 0")
                .LessThanOrEqualTo(BusinessRules.MaxRoomCapacity)
                .WithMessage($"Capacity must not exceed {BusinessRules.MaxRoomCapacity}");
        }
    }
}
