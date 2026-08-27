namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public class OrderTotalPriceCalculateDto
    {
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public List<decimal> ReservedServicePrice {  get; set; }
        public decimal HallPrice { get; set; }
    }
}
