using API_Rdv.Data;
using API_Rdv.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API_Rdv.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SallesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SallesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalleModel>>> GetAll()
        {
            var salles = await _context.Salles.Include(s => s.Creneaux)
                .Select(s => new SalleModel
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Capacite = s.Capacite,
                    Creneaux = s.Creneaux.Select(c => new CreneauModel
                    {
                        Id = c.Id,
                        Debut = c.Debut,
                        Fin = c.Fin,
                        EstReserve = c.EstReserve
                    }).ToList()
                }).ToListAsync();

            return Ok(salles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalleModel>> GetById(int id)
        {
            var salle = await _context.Salles.Include(s => s.Creneaux)
                .Where(s => s.Id == id)
                .Select(s => new SalleModel
                {
                    Id = s.Id,
                    Nom = s.Nom,
                    Capacite = s.Capacite,
                    Creneaux = s.Creneaux.Select(c => new CreneauModel
                    {
                        Id = c.Id,
                        Debut = c.Debut,
                        Fin = c.Fin,
                        EstReserve = c.EstReserve
                    }).ToList()
                }).FirstOrDefaultAsync();

            if (salle == null)
                return NotFound("Salle introuvable.");

            return Ok(salle);
        }

        [HttpGet("{id}/creneaux")]
        public async Task<ActionResult<List<CreneauModel>>> GetCreneaux(int id)
        {
            var salle = await _context.Salles.FindAsync(id);
            if (salle == null)
                return NotFound("Salle introuvable.");

            var creneaux = await _context.Creneaux
                .Where(c => c.SalleId == id)
                .Select(c => new CreneauModel
                {
                    Id = c.Id,
                    Debut = c.Debut,
                    Fin = c.Fin,
                    EstReserve = c.EstReserve
                }).ToListAsync();

            return Ok(creneaux);
        }
    }
}
