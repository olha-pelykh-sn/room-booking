using AutoMapper;
using room_booking_backend.DTOs.RoomDTOs.Requests;
using room_booking_backend.DTOs.RoomDTOs.Responses;
using room_booking_backend.Models;

namespace room_booking_backend.Mappings
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, CreateRoomRequest>().ReverseMap();
            CreateMap<Room, UpdateRoomRequest>().ReverseMap();
            CreateMap<Room, DefaultRoomResponse>().ReverseMap();
        }
    }
}
