using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Repository;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ILogger<TableController> _logger;

        
        private readonly ITableRepository tableRepository;
        public TableController(ITableRepository _tableRepository, ILogger<TableController> logger)
        {
            tableRepository = _tableRepository;
            _logger = logger;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] Table table)
        {
            if (ModelState.IsValid)
            {
                if (table == null)
                {
                    return BadRequest("invalid value");
                }
                tableRepository.Add(table);
                return Ok(table);
            }
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Table> tables = tableRepository.getAll();
            if(tables == null)
            {
                return NotFound("not found");
            }
            return Ok(tables);           
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            Table table = tableRepository.GetById(id);
            if (table == null)
            {
                return NotFound("item not found");
            }
            return Ok(table);
        }
        [HttpPut("Update/{id}")]
        
        public IActionResult Update([FromRoute] int id, [FromBody] Table table)
        {
            if (ModelState.IsValid)
            {
                Table table1 = tableRepository.GetById(id);
                if (table1 == null)
                {
                    return NotFound("item not found");
                }
                if (table == null)
                {
                    return BadRequest("invalid value");
                }
                tableRepository.Update(id, table);
                return NoContent();
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Table table = tableRepository.GetById(id);
            if (table == null)
            {
                return NotFound("item not found");
            }
            tableRepository.Delete(id);
            return NoContent();
        }
    }
}
