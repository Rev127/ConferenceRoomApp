namespace ConferenceRoomAppAPI.Data.Models
{
    public class HallServices
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Halls> Halls { get; set; }
    }
}
