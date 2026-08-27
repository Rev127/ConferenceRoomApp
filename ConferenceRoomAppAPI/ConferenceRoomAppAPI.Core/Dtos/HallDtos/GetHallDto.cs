using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.HallDtos
{
    public class GetHallDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public List<int> HallServicesIds { get; set; }
    }
}
