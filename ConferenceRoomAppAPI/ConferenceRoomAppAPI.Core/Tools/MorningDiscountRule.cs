using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Tools
{
    public class MorningDiscountRule : IPriceDiscountRules
    {
        public bool IsMatch(TimeSlot timeSlot)
        {
            if(timeSlot.StartTime.Hour >= 6 && timeSlot.EndTime.Hour <= 9)
            {
                return true;
            }

            return false;
        }

        public decimal CalculateDiscount(decimal basePrice)
        {
            return basePrice * 0.9m;
        }
    }
}
