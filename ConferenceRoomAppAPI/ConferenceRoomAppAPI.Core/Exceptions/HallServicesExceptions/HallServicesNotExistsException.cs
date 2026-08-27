using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions.HallServicesExceptions
{
    public class HallServicesNotExistsException : ConferenceRoomBaseExeption
    {
        public HallServicesNotExistsException()
            : base("One or more selected services do not exist.", HttpStatusCode.NotFound)
        {
        }
    }
}
