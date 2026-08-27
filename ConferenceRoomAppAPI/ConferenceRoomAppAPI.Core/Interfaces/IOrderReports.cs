using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Dtos.HallDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IOrderReports
    {

        public Task<List<ReportDto>> GetBookingReportsAsync(DateSlot dateSlot);
        public Task<List<PopularHallsDto>> GetPopularHallsAsync();
    }
}
