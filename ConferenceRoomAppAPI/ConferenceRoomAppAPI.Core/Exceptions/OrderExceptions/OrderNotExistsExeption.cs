using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.OrderExceptions
{
    public class OrderNotExistsExeption : ConferenceRoomBaseExeption
    {
        public OrderNotExistsExeption(int orderId)
            : base($"Order with ID {orderId} was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}
