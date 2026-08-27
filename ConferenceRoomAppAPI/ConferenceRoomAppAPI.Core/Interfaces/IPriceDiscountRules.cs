using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IPriceDiscountRules
    {
        public bool IsMatch(TimeSlot slot);
        public decimal CalculateDiscount(decimal basePrice);
    }
}
