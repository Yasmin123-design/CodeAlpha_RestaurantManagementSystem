using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantManagementSystem.Dto;
using RestaurantManagementSystem.Models.Repository;
using RestaurantManagementSystem.Models;
using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReservationRepository reservationRepository;
        private readonly ITableRepository tableRepository;
        public ReservationController(IReservationRepository _reservationRepository,ITableRepository _tableRepository,UserManager<ApplicationUser> manager)
        {
            reservationRepository = _reservationRepository;
            tableRepository = _tableRepository;
            userManager = manager;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            List<Reversation> reversations = reservationRepository.getAll();
            if(reversations == null)
            {
                return NotFound("there no reversations until now");
            }
            List<UserWithTableDto> userWithTableDtos = new List<UserWithTableDto>();
            foreach (var reservation in reversations)
            {
                userWithTableDtos.Add(new UserWithTableDto()
                {
                    UserName = reservation.ApplicationUser.UserName,
                    TableNumber = reservation.Table.TableNumber,
                    ReservationDate = reservation.ReversationDate
                });
            }
            return Ok(userWithTableDtos);

        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            Reversation reversation = reservationRepository.GetById(id);
            if(reversation == null)
            {
                return NotFound("reversation not found");
            }
            UserWithTableDto user = new UserWithTableDto()
            {
                TableNumber = reversation.Table.TableNumber,
                UserName = reversation.ApplicationUser.UserName
            };
            return Ok(user);
        }
        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] Reversation reversation)
        {
            if (ModelState.IsValid)
            {
                Reversation reversation1 = reservationRepository.GetById(id);
                if (reversation1 == null)
                {
                    return NotFound("item not found");
                }
                if (reversation == null)
                {
                    return BadRequest("invalid value");
                }
                reversation.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                reservationRepository.Update(id, reversation);
                return NoContent();
            }
            return BadRequest(ModelState);

        }
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            Reversation reversation = reservationRepository.GetById(id);
            if (reversation == null)
            {
                return NotFound("item not found");
            }
            reservationRepository.Delete(id);
            return NoContent();
        }
        [Authorize]
        [HttpPost("Add/{tableid}")]
        public async Task< IActionResult> Add([FromRoute]int tableid)
        {
            //var user = await userManager.GetUserAsync(User);
            //string userid = user.Id;
            var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Table table = tableRepository.GetById(tableid);
            if(table == null)
            {
                return NotFound("there is no tables");
            }
            reservationRepository.Add(tableid,userid);
            return Ok("reservation done");
        }
    }
}
