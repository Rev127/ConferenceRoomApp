using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Tools
{
    public class PeakTimeDiscountRule : IPriceDiscountRules
    {
        public bool IsMatch(TimeSlot timeSlot)
        {
            if(timeSlot.StartTime.Hour >= 12 && timeSlot.EndTime.Hour <= 14)
            {
                return true;
            }
            return false;
        }

        public decimal CalculateDiscount(decimal basePrice)
        {
            return basePrice * 0.85m;
        }
    }
}
