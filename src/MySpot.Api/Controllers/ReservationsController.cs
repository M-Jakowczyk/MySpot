using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Application.DTOs;
using MySpot.Application.Services;

namespace MySpot.Api.Controllers
{
    //[Route("reservations")]
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(_service.GetAllWeekly());

        [HttpGet("{id:Guid}")]
        public ActionResult<ReservationDto> Get(Guid id)
        {
            var reservation = _service.Get(id);
            if (reservation is null)
                return NotFound();
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(CreateReservation command)
        {
            var id = _service.Create(command with { ReservationId = Guid.NewGuid() });
            if (id is null)
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpPut("{id:Guid}")]
        public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
        {
            if (!_service.Update(command with { ReservationId = id }))
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public ActionResult Delete(Guid id)
        {
            if (!_service.Delete(new DeleteReservation(id)))
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
