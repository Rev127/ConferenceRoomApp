using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Dtos.HallDtos;
using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos
{
    public class PriceCalculateDto
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public GetHallDto Hall { get; set; }
        public List<GetHallServicesDto> ReservedServices { get; set; }
    }
}
