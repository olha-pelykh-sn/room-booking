using FluentValidation;
using room_booking_backend.DTOs.RoomDTOs.Requests;
using System;

public class GetAvailableRoomsRequestValidator : AbstractValidator<GetAvailableRoomsRequest>
{
    public GetAvailableRoomsRequestValidator()
    {
        // Working time definition
        var workStart = TimeSpan.FromHours(10);
        var workEnd = TimeSpan.FromHours(18);

        RuleFor(x => x.CheckIn)
            .NotEmpty().WithMessage("CheckIn date is required")
            .GreaterThan(DateTime.UtcNow).WithMessage("CheckIn must be in the future")
            .Must(checkIn => IsWithinWorkingHours(checkIn, workStart, workEnd))
            .WithMessage("CheckIn time must be between 10:00 and 18:00");

        RuleFor(x => x.CheckOut)
            .NotEmpty().WithMessage("CheckOut date is required")
            .GreaterThan(x => x.CheckIn).WithMessage("CheckOut must be later than CheckIn")
            .Must(checkOut => IsWithinWorkingHours(checkOut, workStart, workEnd))
            .WithMessage("CheckOut time must be between 10:00 and 18:00");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0")
            .LessThanOrEqualTo(100).WithMessage("Capacity must not exceed 100");
    }

    private bool IsWithinWorkingHours(DateTime dateTime, TimeSpan start, TimeSpan end)
    {
        var time = dateTime.TimeOfDay;
        return time >= start && time <= end;
    }
}