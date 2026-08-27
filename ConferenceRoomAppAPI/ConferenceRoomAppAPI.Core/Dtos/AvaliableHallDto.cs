namespace ConferenceRoomAppAPI.Services.Dtos
{
    public class AvaliableHallDto
    {
        public int Capacity { get; set; }

        public DateOnly Date { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
