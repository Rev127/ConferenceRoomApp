using ConferenceRoomAppAPI.Data.Models;
using ConferenceRoomAppAPI.Services.Dtos.HallDtos;
using ConferenceRoomAppAPI.Services.Dtos;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;
using ConferenceRoomAppAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConferenceRoomAppAPI.Controllers
{
    [ApiController]
    [Route("api/conference-rooms")]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly  IHallsServices _hallsServices;
        private readonly IOrdersServices _ordersServices;

        public ConferenceRoomController(IHallsServices hallsServices, IOrdersServices ordersServices)
        {
            _hallsServices = hallsServices;
            _ordersServices = ordersServices;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateConferenceRoom([FromBody] CreateHallDto createHallDto)
        {
            await _hallsServices.CreateHallAsync(createHallDto);
            return Created();
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateConferenceRoom([FromBody] UpdateHallDto updateHallDto)
        {
            await _hallsServices.UpdateHallAsync(updateHallDto);
            return Ok();
        }

        [HttpDelete("delete/{hallId}")]
        public async Task<IActionResult> DeleteConferenceRoom([FromRoute] int hallId)
        {
            await _hallsServices.DeleteHallAsync(hallId);
            return Ok();
        }

        [HttpGet("available")]
        public async Task<IActionResult> GetAvailableConferenceRooms([FromQuery] AvaliableHallDto avaliableHallDto)
        {
            var halls = await _hallsServices.GetAllHallsAsync();
            var availableRooms = halls
                .Where(h => h.Capacity >= avaliableHallDto.Capacity && _ordersServices
                .IsHallAvailable(h.Id, new DateTimeSlot
                (
                    avaliableHallDto.Date,
                    avaliableHallDto.StartTime,
                    avaliableHallDto.EndTime
                )));

            return Ok(availableRooms);
        }

        [HttpPost("booking")]
        public async Task<IActionResult> BookConferenceRoom([FromBody] CreateOrderDto createOrderDto)
        {
            var totalPrice = await _ordersServices.CreateOrderAsync(createOrderDto);
            return CreatedAtAction(nameof(BookConferenceRoom), new { totalPrice });
        }
    }
}
