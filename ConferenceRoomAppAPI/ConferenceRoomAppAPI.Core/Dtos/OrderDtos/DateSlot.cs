using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public record DateSlot(
        DateOnly StartDate,
        DateOnly EndDate
    );
}
