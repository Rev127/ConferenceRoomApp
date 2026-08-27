using ConferenceRoomAppAPI.Services.Dtos;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Interfaces;

namespace ConferenceRoomAppAPI.Services.Tools
{
    public class PriceCalculator : IPriceCalculator
    {
        private readonly IEnumerable<IPriceDiscountRules> _discountRules;

        public PriceCalculator(IEnumerable<IPriceDiscountRules> discountRules)
        {
            _discountRules = discountRules;
        }
        public decimal CalculateTotalPrice(PriceCalculateDto calculateDto)
        {
            decimal totalPrice = calculateDto.ReservedServices
                .Select(s => s.Price)
                .Sum();

            decimal hallPrice = calculateDto.Hall.PricePerHour * (decimal)(calculateDto.EndTime - calculateDto.StartTime).TotalHours;

            var priceDiscountRule = _discountRules
                .FirstOrDefault(rule => rule.IsMatch(new TimeSlot(calculateDto.StartTime, calculateDto.EndTime)));

            if (priceDiscountRule is not null)
            {
                totalPrice += priceDiscountRule.CalculateDiscount(hallPrice);
            }

            return totalPrice;
        }
    }
}
