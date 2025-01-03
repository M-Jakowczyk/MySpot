using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;
namespace MySpot.Api.Controllers
{
    //[Route("reservsations")]
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _service = new();

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = _service.Get(id);
            if(reservation is null)
                return NotFound();
            return Ok(reservation);
        } 
        
        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var id = _service.Create(reservation);
            if(id is null)            
                return BadRequest();
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            if(!_service.Update(id,reservation))            
                return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            if(!_service.Delete(id))            
                return NotFound();
            return NoContent();
        }
    }
}
