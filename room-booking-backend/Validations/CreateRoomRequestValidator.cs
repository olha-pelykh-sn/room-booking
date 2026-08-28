using FluentValidation;
using room_booking_backend.DTOs.RoomDTOs.Requests;

namespace room_booking_backend.Validations
{
    public class CreateRoomRequestValidator : AbstractValidator<CreateRoomRequest>
    {
        public CreateRoomRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required")
                .GreaterThan(0).WithMessage("Price must be greater than 0");

            RuleFor(x => x.Capacity)
                .NotNull().WithMessage("Capacity is required")
                .GreaterThan(0).WithMessage("Capacity must be greater than 0")
                .LessThanOrEqualTo(1000).WithMessage("Capacity must not exceed 100");
        }
    }
}
