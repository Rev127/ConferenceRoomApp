using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public record DateTimeSlot(
        DateOnly Date,
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
