using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;

namespace ConferenceRoomAppAPI.Services.Dtos.HallDtos
{
    public class CreateHallDto
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }
        public List<CreateHallServicesDto> HallServices { get; set; }
    }
}
