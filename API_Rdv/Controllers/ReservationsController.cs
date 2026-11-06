using API_Rdv.Data;
using API_Rdv.Dto;
using API_Rdv.Entities;
using API_Rdv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Rdv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationModel>>> GetAll()
        {
            var reservations = await _context.Reservations
                .Select(r => new ReservationModel
                {
                    Id = r.Id,
                    SalleId = r.SalleId,
                    CreneauId = r.CreneauId,
                    UserId = r.UserId,
                    DateReservation = r.DateReservation
                }).ToListAsync();

            return Ok(reservations);
        }

        [HttpPost]
        public async Task<ActionResult<ReservationModel>> Create([FromBody] ReservationRequestDto dto)
        {
            var salle = await _context.Salles.Include(s => s.Creneaux).FirstOrDefaultAsync(s => s.Id == dto.SalleId);
            if (salle == null)
                return NotFound("Salle introuvable.");

            // Recherche le créneau correspondant aux horaires demandés
            var creneau = salle.Creneaux.FirstOrDefault(c => c.Debut == dto.Debut && c.Fin == dto.Fin);
            if (creneau == null)
                return NotFound("Aucun créneau disponible pour ces horaires.");

            if (creneau.EstReserve)
                return BadRequest("Ce créneau est déjà réservé.");

            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound("Utilisateur introuvable.");

            creneau.EstReserve = true;

            var reservation = new Reservation
            {
                SalleId = dto.SalleId,
                CreneauId = creneau.Id,
                UserId = dto.UserId
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            var model = new ReservationModel
            {
                Id = reservation.Id,
                SalleId = reservation.SalleId,
                CreneauId = reservation.CreneauId,
                UserId = reservation.UserId,
                DateReservation = reservation.DateReservation
            };

            return CreatedAtAction(nameof(GetAll), new { id = model.Id }, model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id, [FromQuery] int userId)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
                return NotFound("Réservation introuvable.");

            if (reservation.UserId != userId)
                return StatusCode(403, "Vous ne pouvez annuler que vos propres réservations.");

            // Libère le créneau
            var creneau = await _context.Creneaux.FindAsync(reservation.CreneauId);
            if (creneau != null)
                creneau.EstReserve = false;

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
