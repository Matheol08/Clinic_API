using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsMedecins;
using ModelsRendezVous;

namespace API_rest.ContrôleursMedecins
{
    [ApiController]
    [Route("api/Medecins")]

    public class MedecinsController : ControllerBase
    {
        private readonly ClinicContext _contextMedecins;

        public MedecinsController(ClinicContext context)
        {
            _contextMedecins = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Medecins>>> GetMedecins()
        {
            var Medecins = await _contextMedecins.Medecins
           .Include(s => s.Specialites)
           .ToListAsync();
            return await _contextMedecins.Medecins.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Medecins>> GetMedecinsparId(int id)
        {
            var Medecins = await _contextMedecins.Medecins.Where(c => c.IdMedecin.Equals(id)).FirstOrDefaultAsync();
            if (Medecins == null)
            {
                return NotFound();
            }
            return Ok(Medecins);
        }

        [HttpPost]
        public async Task<ActionResult<Medecins>> CreateMedecins(AjoutMedecins Medecins)
        {
            Medecins ajoutmdc = new Medecins
            {
                Nom = Medecins.Nom,
                Prenom = Medecins.Prenom,
                SpecialiteId = Medecins.IdSpecialite
            };
            _contextMedecins.Medecins.Add(ajoutmdc);
            await _contextMedecins.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedecinsparId), new { id = ajoutmdc.IdMedecin }, ajoutmdc);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedecins(int id)
        {
            var Medecins = await _contextMedecins.Medecins.FindAsync(id);
            if (Medecins == null)
            {
                return NotFound();
            }
            _contextMedecins.Medecins.Remove(Medecins);
            await _contextMedecins.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMedecins(int id, majMedecins Medecins)
        {
            if (id != Medecins.IdMedecin)
            {
                return BadRequest("IDs are different");
            }

            var medecinToUpdate = await _contextMedecins.Medecins.FindAsync(id);
            if (medecinToUpdate == null)
            {
                return NotFound($"Medecin with Id = {id} not found");
            }

            // Mettez à jour les propriétés du médecin avec les nouvelles valeurs
            medecinToUpdate.Nom = Medecins.Nom;
            medecinToUpdate.Prenom = Medecins.Prenom;
            medecinToUpdate.SpecialiteId = Medecins.SpecialiteId; // Mettez à jour l'ID de la spécialité

            try
            {
                await _contextMedecins.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedecinsExists(id))
                {
                    return NotFound($"Medecin with Id = {id} not found");
                }
                else
                {
                    throw;
                }
            }
        }

        private bool MedecinsExists(int id)
        {
            return _contextMedecins.Medecins.Any(e => e.IdMedecin == id);
        }

    }
}
