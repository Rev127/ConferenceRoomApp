using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.HallExceptions
{
    public class HallNotExistsException : ConferenceRoomBaseExeption
    {
        public HallNotExistsException(int hallId)
            : base($"Hall with ID {hallId} was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}
