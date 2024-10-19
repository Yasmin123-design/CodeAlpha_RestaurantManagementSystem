using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Models;
using RestaurantManagementSystem.Models.Repository;
using System.ComponentModel.Design;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository menuRepository;
        public MenuController(IMenuRepository _menuRepository)
        {
            menuRepository = _menuRepository;
        }
        [HttpPost("Add")]
        public IActionResult Add([FromBody]MenuItem menuItem)
        {
            if (ModelState.IsValid)
            {
                if (menuItem == null)
                {
                    return BadRequest("invalid value");
                }
                menuRepository.Add(menuItem);
                return Ok(menuItem);
            }
            return BadRequest(ModelState);
        }
        [HttpGet("GetAll")]
        // [Authorize]
        public IActionResult GetAll()
        {
            List<MenuItem> items = menuRepository.getAll();
            if(items == null)
            {
                return NotFound("not found");
            }
            return Ok(items);
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            MenuItem item = menuRepository.GetById(id);
            if(item == null)
            {
                return NotFound("item not found");
            }
            return Ok(item);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute]int id ,[FromBody] MenuItem item)
        {
            if (ModelState.IsValid)
            {
                MenuItem item1 = menuRepository.GetById(id);
                if (item1 == null)
                {
                    return NotFound("item not found");
                }
                if (item == null)
                {
                    return BadRequest("invalid value");
                }
                menuRepository.Update(id, item);
                return NoContent();
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            MenuItem item = menuRepository.GetById(id);
            if(item == null)
            {
                return NotFound("item not found");
            }
            menuRepository.Delete(id);
            return NoContent();
        }
    }
}
