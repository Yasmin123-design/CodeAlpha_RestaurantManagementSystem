using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Repository;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository orderItem;
        public OrderItemController(IOrderItemRepository _orderItem)
        {
            orderItem = _orderItem;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody] OrderItem menuItem)
        {
            if (ModelState.IsValid)
            {
                if (menuItem == null)
                {
                    return BadRequest("invalid value");
                }
                orderItem.Add(menuItem);
                return Ok(menuItem);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<OrderItem> items = orderItem.getAll();
            if(items == null)
            {
                return NotFound("not found");
            }
            return Ok(items);
        }
        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            OrderItem item = orderItem.GetById(id);
            if (item == null)
            {
                return NotFound("item not found");
            }
            return Ok(item);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] OrderItem item)
        {
            if (ModelState.IsValid)
            {
                OrderItem item1 = orderItem.GetById(id);
                if (item1 == null)
                {
                    return NotFound("item not found");
                }
                if (item == null)
                {
                    return BadRequest("invalid value");
                }
                orderItem.Update(id, item);
                return NoContent();
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            OrderItem item = orderItem.GetById(id);
            if (item == null)
            {
                return NotFound("item not found");
            }
            orderItem.Delete(id);
            return NoContent();
        }
    }
}
