using ConferenceRoomAppAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ConferenceRoomAppAPI.Services
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly HttpContext _context;

        public CurrentUserServices(IHttpContextAccessor context)
        {
            _context = context.HttpContext;
        }

        public string GetCurrentUser()
        {
            var userId = _context.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId is null)
            {
                throw new UnauthorizedAccessException("Користувач не автентифікований.");
            }

            return userId;
        }
    }
}
