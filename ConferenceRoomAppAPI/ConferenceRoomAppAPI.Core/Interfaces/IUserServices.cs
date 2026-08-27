using ConferenceRoomAppAPI.Services.Dtos.UserDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IUserServices
    {
        public Task<List<GetUserDtos>> GetUsersByNameAsync(string name);
        public Task<List<GetUserDtos>> GetAllUsersAsync();
    }
}
