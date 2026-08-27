namespace ConferenceRoomAppAPI.Data.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int HallId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalPrice { get; set; }
        public Users User { get; set; }
        public Halls Hall { get; set; }

        public ICollection<HallServices> ReservedServices { get; set; }
    }
}
