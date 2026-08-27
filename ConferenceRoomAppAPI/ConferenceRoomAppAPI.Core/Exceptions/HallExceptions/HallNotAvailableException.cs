using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.HallExceptions
{
    public class HallNotAvailableException : ConferenceRoomBaseExeption
    {
        public HallNotAvailableException() : base("The selected hall is not available for the specified date and time.", HttpStatusCode.NotFound) { }
    }
}
