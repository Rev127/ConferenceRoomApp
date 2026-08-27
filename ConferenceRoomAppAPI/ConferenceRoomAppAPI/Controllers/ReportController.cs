using Microsoft.AspNetCore.Mvc;
using ConferenceRoomAppAPI.Services.Interfaces;
using ConferenceRoomAppAPI.Services.Dtos.OrderDtos;

namespace ConferenceRoomAppAPI.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController : ControllerBase
    {
        private readonly IOrderReports _orderReports;
        public ReportController(IOrderReports orderReports)
        {
            _orderReports = orderReports;
        }

        [HttpGet("booking")]
        public async Task<IActionResult> GetBookingReports([FromQuery] DateSlot dateSlot)
        {
            var reports = await _orderReports.GetBookingReportsAsync(dateSlot);
            return Ok(reports);
        }

        [HttpGet("popular-halls")]
        public async Task<IActionResult> GetPopularHalls()
        {
            var popularHalls = await _orderReports.GetPopularHallsAsync();
            return Ok(popularHalls);
        }

    }
}
