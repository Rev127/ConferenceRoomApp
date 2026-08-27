using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;

namespace ConferenceRoomAppAPI.Services.Dtos.HallDtos
{
    public class UpdateHallDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Capacity { get; set; }
        public decimal? PricePerHour { get; set; }
        public List<GetHallServicesDto>? HallServices { get; set; }
    }
}
