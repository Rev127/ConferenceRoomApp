using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.HallExceptions
{
    public class HallAlreadyExistsException : ConferenceRoomBaseExeption
    {
        public HallAlreadyExistsException(string hallName)
            : base($"Hall with name '{hallName}' already exists.", HttpStatusCode.NotFound)
        {
        }
    }
}
