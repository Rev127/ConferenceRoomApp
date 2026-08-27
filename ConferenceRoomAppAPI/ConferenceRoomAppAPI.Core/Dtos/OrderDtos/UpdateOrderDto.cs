using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public DateOnly? Date { get; set; }
        public TimeOnly? StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public List<int>? ReservedServiceIds { get; set; }
    }
}
