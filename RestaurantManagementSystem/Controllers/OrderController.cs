using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Repository;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        public OrderController(IOrderRepository _orderRepository)
        {
            orderRepository = _orderRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Order> orders = orderRepository.getAll();
            if(orders == null)
            {
                return NotFound("not found");
            }
            return Ok(orders);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            Order order = orderRepository.GetById(id);
            if(order == null)
            {
                return NotFound("order not found");
            }
            return Ok(order);
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody]Order order)
        {
            if (ModelState.IsValid)
            {
                if (order == null)
                {
                    return BadRequest("invalid value");
                }
                
                orderRepository.Add(order);
                return Ok(order);
            }
            return BadRequest(ModelState);
        }
        [HttpPut("Update")]
        public IActionResult Update(int id , [FromBody] Order order)
        {
            if (ModelState.IsValid)
            {
                Order order1 = orderRepository.GetById(id);
                if (order1 == null)
                {
                    return NotFound("order not found you need to updated");
                }
                if (order == null)
                {
                    return BadRequest("invalid value");
                }
                orderRepository.Update(id, order);
                return NoContent();
            }
            return BadRequest(ModelState);
        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            Order order = orderRepository.GetById(id);
            if(order == null)
            {
                return NotFound("order not found");
            }
            orderRepository.Delete(id);
            return NoContent();
        }
    }
}
