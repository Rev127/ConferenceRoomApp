namespace ConferenceRoomAppAPI.Services.Dtos.OrderDtos
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int HallId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public List<int> ReservedServiceIds { get; set; }
    }
}
