using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Tools
{
    public class EveningDiscountRule : IPriceDiscountRules
    {
        public bool IsMatch(TimeSlot timeSlot)
        {
            if(timeSlot.StartTime.Hour >= 18 && timeSlot.EndTime.Hour <= 23)
            {
                return true;
            }

            return false;
        }

        public decimal CalculateDiscount(decimal basePrice)
        {
            return basePrice * 0.8m;
        }
    }
}
