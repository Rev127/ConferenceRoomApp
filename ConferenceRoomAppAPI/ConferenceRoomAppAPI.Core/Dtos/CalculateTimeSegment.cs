using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos
{
    public record CalculateTimeSegment(
        float percentage,
        TimeOnly StartTime,
        TimeOnly EndTime
    );
}
