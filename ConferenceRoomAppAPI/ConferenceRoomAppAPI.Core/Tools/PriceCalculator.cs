using ConferenceRoomAppAPI.Services.Dtos;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using System.Timers;

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

            decimal hallPrice = calculateDto.Hall.PricePerHour;

            totalPrice += this.GetHallTimePrices(hallPrice, new TimeSlot(calculateDto.StartTime, calculateDto.EndTime));


            return Math.Round(totalPrice, 2);
        }

        private decimal GetHallTimePrices(decimal hallPrice, TimeSlot timeSlot)
        {
            decimal totalPrice = 0;
            var time = timeSlot.StartTime;
            List<CalculateTimeSegment> segments = this.GetCalculateTimeSegments(timeSlot);

            foreach (var segment in segments) {
                decimal currentHallPrice = hallPrice;
                if (segment.percentage != 0)
                {
                    currentHallPrice *= (decimal)segment.percentage;
                }

                var priceDiscountRule = _discountRules
                    .FirstOrDefault(rule => rule.IsMatch(new TimeSlot(segment.StartTime, segment.EndTime)));

                if (priceDiscountRule is not null)
                {
                    totalPrice += priceDiscountRule.CalculateDiscount(currentHallPrice);
                }
                else
                {
                    totalPrice += currentHallPrice;
                }
            }

            return totalPrice;
        }

        private List<CalculateTimeSegment> GetCalculateTimeSegments(TimeSlot timeSlot)
        {
            var segments = new List<CalculateTimeSegment>();
            var time = timeSlot.StartTime;

            while (time.Hour < timeSlot.EndTime.Hour)
            {
                var startSegment = new CalculateTimeSegment
                (
                    (float)(60 - time.Minute) / 60f,
                    new TimeOnly(time.Hour, 0),
                    new TimeOnly(time.Hour + 1, 0)
                );
                segments.Add(startSegment);
                time = new TimeOnly(time.Hour + 1, 0);
            }

            if (timeSlot.EndTime.Minute != 0)
            {
                CalculateTimeSegment endSegment = new CalculateTimeSegment(
                    (float)(timeSlot.EndTime.Minute) / 60f,
                    new TimeOnly(timeSlot.EndTime.Hour, 0),
                    new TimeOnly(timeSlot.EndTime.Hour + 1, 0)
                );
                segments.Add(endSegment);
            }

            return segments;
        }
    }
}
