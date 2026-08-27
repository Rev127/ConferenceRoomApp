using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.UserExceptions
{
    public class UserNotFoundException : ConferenceRoomBaseExeption
    {
        public UserNotFoundException() : base("User not found", HttpStatusCode.NotFound)
        {
        }
    }
}
