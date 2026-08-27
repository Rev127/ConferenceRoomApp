using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Services.Interfaces
{
    public interface IOrdersServices
    {
        public Task<GetOrderDto> GetOrderByIdAsync(int id);
        public Task<List<GetOrderDto>> GetAllOrderAsync();
        public Task<decimal> CreateOrderAsync(CreateOrderDto createOrderDto);
        public Task UpdateOrderAsync(UpdateOrderDto updateorderDto);
        public Task DeleteOrderAsync(int id);
        public bool IsHallAvailable(int hallId, DateTimeSlot dateTimeSlot);

    }
}
