using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Tools
{
    public class StandartDiscountRule : IPriceDiscountRules
    {
        public bool IsMatch(TimeSlot slot)
        {
            if(slot.StartTime.Hour >= 9 && slot.EndTime.Hour <= 18)
            {
                return true;
            }
            return false;
        }

        public decimal CalculateDiscount(decimal basePrice)
        {
            return basePrice;
        }
    }
}
