using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.OrderExceptions
{
    public class OrderInvalidTimeRangeException : ConferenceRoomBaseExeption
    {
        public OrderInvalidTimeRangeException() : base("The time range for the order is invalid. Using a valid time range is required (6:00 to 23:00).", HttpStatusCode.Forbidden)
        {
        }
    }
}
