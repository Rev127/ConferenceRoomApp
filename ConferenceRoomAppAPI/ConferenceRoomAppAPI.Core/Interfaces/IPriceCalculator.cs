using ConferenceRoomAppAPI.Services.Dtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IPriceCalculator
    {
        public decimal CalculateTotalPrice(PriceCalculateDto calculateDto);
    }
}
