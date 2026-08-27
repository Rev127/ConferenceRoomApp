using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public record TimeSlot(
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
