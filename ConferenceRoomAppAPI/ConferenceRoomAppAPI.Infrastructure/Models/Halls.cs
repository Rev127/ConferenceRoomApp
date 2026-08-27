namespace ConferenceRoomAppAPI.Data.Models
{
    public class Halls
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }

        public ICollection<Orders> Orders { get; set; }

        public ICollection<HallServices> Services { get; set; }

    }
}
