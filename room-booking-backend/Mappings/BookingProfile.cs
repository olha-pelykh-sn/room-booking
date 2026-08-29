using AutoMapper;
using room_booking_backend.DTOs.BookingDTOs.Responses;
using room_booking_backend.Models;

namespace room_booking_backend.Mappings
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<Service, ServiceInfo>();
        }
    }
}
