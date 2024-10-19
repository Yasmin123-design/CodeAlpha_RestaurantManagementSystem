using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Repository;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository reportRepository;
        public ReportController(IReportRepository _reportRepository)
        {
            reportRepository = _reportRepository;
        }
        [HttpGet("GetOrdersReport")]
        public IActionResult GetOrdersReport([FromQuery]DateTime starttime ,[FromQuery] DateTime endtime)
        {
            List<Order> orders = reportRepository.GetOrderReport(starttime, endtime);
            if(orders == null)
            {
                return NotFound("not found");
            }
            return Ok(orders);
        }
        [HttpGet("GetReservationReport")]
        public IActionResult GetReservationReport([FromQuery]DateTime starttime , [FromQuery]DateTime endtime)
        {
            List<Reversation> reversations = reportRepository.GetReversationReport(starttime, endtime);
            if(reversations == null)
            {
                return NotFound("not found");
            }
            return Ok(reversations);
        }
        [HttpGet("GetInventoryReport")]
        public IActionResult GetInventoryReport()
        {
            List<Inventory> inventories = reportRepository.GetInventoryReport();
            if(inventories == null)
            {
                return NotFound("not found");
            }
            return Ok(inventories);
        }
    }
}
