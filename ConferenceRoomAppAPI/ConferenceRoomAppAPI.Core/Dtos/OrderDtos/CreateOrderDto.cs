using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int HallId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public List<int> ReservedServicesIds { get; set; }
    }
}
