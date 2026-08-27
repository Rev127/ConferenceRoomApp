using Microsoft.AspNetCore.Mvc;
using ConferenceRoomAppAPI.Services.Interfaces;

namespace ConferenceRoomAppAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserControler : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserControler(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userServices.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            var user = await _userServices.GetUsersByNameAsync(name);
            return Ok(user);
        }
    }
}
