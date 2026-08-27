using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IHallServices
    {
        public Task<GetHallServicesDto> GetHallServicesByIdAsync(int hallServicesId);
        public Task<List<GetHallServicesDto>> GetAllHallServicesAsync();
        public Task CreateHallServicesAsync(CreateHallServicesDto hallServicesDto);
        public Task UpdateHallServicesAsync(UpdateHallServicesDto hallServicesDto);
        public Task DeleteHallServicesAsync(int hallServicesId);
    }
}
