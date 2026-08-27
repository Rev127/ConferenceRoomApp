using ConferenceRoomAppAPI.Services.Dtos.HallDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IHallsServices
    {
        public Task<GetHallDto> GetHallByIdAsync(int hallId);
        public Task<List<GetHallDto>> GetAllHallsAsync();
        public Task<int> CreateHallAsync(CreateHallDto hallDto);
        public Task UpdateHallAsync(UpdateHallDto hallDto);
        public Task DeleteHallAsync(int hallId);

    }
}
