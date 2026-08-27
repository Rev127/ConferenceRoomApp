using System.Net;

namespace ConferenceRoomAppAPI.Services.Exceptions
{
    public class ConferenceRoomBaseExeption : Exception
    {
        private HttpStatusCode statusCode;
        public ConferenceRoomBaseExeption(string message, HttpStatusCode statusCode) : base(message)
        {
            this.statusCode = statusCode;
        }

        public HttpStatusCode GetStatusCode()
        {
            return this.statusCode;
        }
    }
}
