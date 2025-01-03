using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
namespace MySpot.Api.Controllers
{
    //[Route("reservsations")]
    [Route("[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private int _id = 1;
        private static readonly List<Reservation> Reservations = new();
        private static readonly List<string> ParkingSpotName = new()
        { "P1", "P2","P3","P4","P5"};

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get() => Ok(Reservations);

        [HttpGet("{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = Reservations.SingleOrDefault(x => x.Id == id);
            if (reservation is null)
            {
                //HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return NotFound();
            }
            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            if(ParkingSpotName.All(x => x != reservation.ParkingSpotName))
            {
                //HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return BadRequest();
            }

            reservation.Date = DateTime.UtcNow.AddDays(1).Date;

            var reservationAlreadyExists = Reservations.Any(x => 
                x.ParkingSpotName == reservation.ParkingSpotName &&
                x.Date.Date == reservation.Date.Date);

            if (reservationAlreadyExists)
            {
                //HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                return BadRequest();
            }

            reservation.Id = _id;
            Reservations.Add(reservation);
            ParkingSpotName.Any(x => x == reservation.ParkingSpotName);
            _id++;
            return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Reservation reservation){
            var reservationToUpdate = Reservations.SingleOrDefault(x => x.Id == id);
            if(reservationToUpdate is null){
                return BadRequest();
            }
            reservationToUpdate.LicensePlate = reservation.LicensePlate;
            
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var reservationToDelete = Reservations.SingleOrDefault(x => x.Id == id);
            if(reservationToDelete is null){
                return BadRequest();
            }
           
            Reservations.Remove(reservationToDelete);
            return NoContent();
        }
    }
}
