using ConferenceRoomAppAPI.Data.Context;
using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Dtos.HallServicesDtos;
using ConferenceRoomAppAPI.Services.Dtos;
using ConferenceRoomAppAPI.Services.Exceptions.HallExceptions;
using ConferenceRoomAppAPI.Services.Exceptions.HallServicesExceptions;
using ConferenceRoomAppAPI.Services.Exceptions.OrderExceptions;
using ConferenceRoomAppAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using ConferenceRoomAppAPI.Services.Dtos.HallDtos;

namespace ConferenceRoomAppAPI.Services.Services
{
    public class OrderServices : IOrdersServices, IOrderReports
    {
        private readonly ConferenceRoomContext _context;
        private readonly IHallsServices _hallServices;
        private readonly IPriceCalculator _priceCalculator;
        private readonly IHallServicesServices _hallServicesServices;
        private readonly ICurrentUserServices _currentUserServices;
        public OrderServices(ConferenceRoomContext context, IHallsServices hallServices, IPriceCalculator priceCalculator, IHallServicesServices hallServicesServices, ICurrentUserServices currentUserServices)
        {
            _context = context;
            _hallServices = hallServices;
            _priceCalculator = priceCalculator;
            _hallServicesServices = hallServicesServices;
            _currentUserServices = currentUserServices;
        }

        public bool IsHallAvailable(int hallId, DateTimeSlot dateTimeSlot)
        {
            var orders = _context.Orders
                .Where(o => o.HallId == hallId && o.Date == dateTimeSlot.Date)
                .ToList();
            foreach (var order in orders)
            {
                if ((dateTimeSlot.StartTime >= order.StartTime && dateTimeSlot.StartTime < order.EndTime) ||
                    (dateTimeSlot.EndTime > order.StartTime && dateTimeSlot.EndTime <= order.EndTime) ||
                    (dateTimeSlot.StartTime <= order.StartTime && dateTimeSlot.EndTime >= order.EndTime))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidTimeRange(TimeOnly startTime, TimeOnly endTime)
        {
            return startTime < endTime && startTime.Hour >= 6 && endTime.Hour <= 23;
        }

        private GetOrderDto GetOrderDtoByIdInternalAsync(Orders order)
        {
            return new GetOrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                HallId = order.HallId,
                Date = order.Date,
                StartTime = order.StartTime,
                EndTime = order.EndTime,
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                ReservedServiceIds = order.ReservedServices.Select(s => s.Id).ToList()
            };
        }

        private async Task<Orders> GetOrderByIdInternalAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order is null)
            {
                throw new OrderNotExistsExeption(id);
            }
            return order;
        }

        public async Task<decimal> CreateOrderAsync(CreateOrderDto orderDto)
        {
            // Validate the time range
            if (!this.IsValidTimeRange(orderDto.StartTime, orderDto.EndTime))
            {
                throw new OrderInvalidTimeRangeException();
            }

            // Check if the hall and hall services exist
            var hall = await _hallServices.GetHallByIdAsync(orderDto.HallId);
            var reservedServiceIds = hall.HallServicesIds
                .Where(s => orderDto.ReservedServicesIds.Contains(s))
                .ToList();

            var reservedServices = await _context.HallServices
                .Where(s => reservedServiceIds.Contains(s.Id))
                .ToListAsync();

            if (!this.IsHallAvailable(orderDto.HallId, new DateTimeSlot
            (
                orderDto.Date,
                orderDto.StartTime,
                orderDto.EndTime
            )))
            {
                throw new HallNotAvailableException();
            }

            // Calculate the total price using the PriceCalculator tool
            decimal totalPrice = _priceCalculator.CalculateTotalPrice(new PriceCalculateDto
            {
                StartTime = orderDto.StartTime,
                EndTime = orderDto.EndTime,
                Hall = hall,
                ReservedServices = reservedServices.Select(s => new GetHallServicesDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Price = s.Price
                }).ToList()
            });

            var order = new Orders
            {
                UserId = _currentUserServices.GetCurrentUser(),
                HallId = orderDto.HallId,
                Date = orderDto.Date,
                StartTime = orderDto.StartTime,
                EndTime = orderDto.EndTime,
                TotalPrice = totalPrice,
                ReservedServices = reservedServices
            };

            await _context.AddAsync(order);
            await _context.SaveChangesAsync();

            return totalPrice;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await this.GetOrderByIdInternalAsync(id);

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetOrderDto>> GetAllOrderAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders.Select(order => this.GetOrderDtoByIdInternalAsync(order)).ToList();
        }

        public async Task<GetOrderDto> GetOrderByIdAsync(int id)
        {
            var order = await this.GetOrderByIdInternalAsync(id);

            return this.GetOrderDtoByIdInternalAsync(order);
        }

        public async Task UpdateOrderAsync(UpdateOrderDto updateOrderDto)
        {
            var order = await this.GetOrderByIdInternalAsync(updateOrderDto.Id);

            // Update the order properties based on the provided DTO
            if (updateOrderDto.Date is not null)
            {
                order.Date = updateOrderDto.Date.Value;
            }

            // Update the start and end times if provided
            if (updateOrderDto.StartTime is not null)
            {
                order.StartTime = updateOrderDto.StartTime.Value;
            }

            // Update the end time if provided
            if (updateOrderDto.EndTime is not null)
            {
                order.EndTime = updateOrderDto.EndTime.Value;
            }

            // Update the reserved services if provided
            var existingServices = _context.HallServices
                .Where(s => updateOrderDto.ReservedServiceIds.Contains(s.Id));

            if (!existingServices.Any() || existingServices.Count() != updateOrderDto.ReservedServiceIds.Count())
            {
                throw new HallServicesNotExistsException();
            }

            order.ReservedServices = existingServices.ToList();

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReportDto>> GetBookingReportsAsync(DateSlot dateSlot)
        {
            var orders = _context.Orders
                .Where(o => o.Date >= dateSlot.StartDate && o.Date <= dateSlot.EndDate)
                .ToList();

            var halls = await _hallServices.GetAllHallsAsync();

            // Group orders by HallId and calculate the total price for each hall
            var reportDtos = orders
                .GroupBy(o => o.HallId)
                .Select(g => new ReportDto
                {
                    HallName = halls.FirstOrDefault(h => h.Id == g.Key).Name,
                    Price = g.Sum(o => o.TotalPrice)
                })
                .ToList();

            return reportDtos;
        }

        public async Task<List<PopularHallsDto>> GetPopularHallsAsync()
        {
            var orders = await _context.Orders.ToListAsync();
            var halls = await _hallServices.GetAllHallsAsync();

            // Group orders by HallId and count the number of bookings for each hall
            var popularHalls = orders
                .GroupBy(o => o.HallId)
                .Select(g => new PopularHallsDto
                {
                    HallName = halls.FirstOrDefault(h => h.Id == g.Key).Name,
                    BookingCount = g.Count()
                })
                .OrderByDescending(p => p.BookingCount)
                .Take(5)
                .ToList();


            return popularHalls;
        }
    }
}
