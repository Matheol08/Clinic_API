using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using ModelsRendezVous;
using ModelsMedecins;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure;
using ModelsPatients;
using Microsoft.IdentityModel.Tokens;

namespace RendezVousController
{
    [ApiController]
    [Route("api/RendezVous")]
    public class RendezVousController : ControllerBase
    {
        private readonly ClinicContext _RendezVousContext;

        public RendezVousController(ClinicContext context)
        {
            _RendezVousContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVous()
        {
            var RendezVousAvecPatientsETMedecins = await _RendezVousContext.RendezVous
                .Include(s => s.Patients)
                .Include(s => s.Medecins)
                .ToListAsync();

            return Ok(RendezVousAvecPatientsETMedecins);
        }


        [HttpGet("MedecinEnRendezVous")]
        public async Task<ActionResult<bool>> MedecinEnRendezVous(int medecinId, DateTime startDateTime, DateTime endDateTime)
        {
            try
            {
                var existingAppointments = await _RendezVousContext.RendezVous
                    .Where(r => r.MedecinId == medecinId &&
                                r.DateDebut < endDateTime &&
                                r.DateFin > startDateTime)
                    .ToListAsync();

                bool isAvailable = existingAppointments.Count == 0;

                return Ok(isAvailable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }



        [HttpGet("DateRange")]
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVoussByDateRange(DateTime startDate, DateTime endDate)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            query = query.Where(s => s.DateDebut >= startDate && s.DateFin <= endDate);

            var result = await query.Include(s => s.Patients).Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }



        [HttpGet("recherchePatient")]
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVousByPatients(string PatientNom)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            if (!string.IsNullOrEmpty(PatientNom))
            {
                
                query = query.Where(s => s.Patients.Nom == PatientNom);
            }

            var result = await query.Include(s => s.Patients).Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }

        [HttpGet("recherchePatientsEtMedecins")]
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVoussByMedcinsetPatients(string patientNom, string medecinNom)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            if (!string.IsNullOrEmpty(patientNom))
            {
                query = query.Where(s => s.Patients.Nom == patientNom);
            }

            if (!string.IsNullOrEmpty(medecinNom))
            {
                query = query.Where(s => s.Medecins.Nom == medecinNom);
            }

            var result = await query.Include(s => s.Patients).Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }




        [HttpGet("rechercheMedecin")] //Recherche par Medecin
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVousparMedecin(string medecinNom)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;


            if (!string.IsNullOrEmpty(medecinNom))
            {
                query = query.Where(s => s.Medecins.Nom == medecinNom);
            }



            var result = await query.Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RendezVous>> GetRendezVousById(int id)
        {
            var RendezVous = await _RendezVousContext.RendezVous.Where(c => c.IdRendezVous.Equals(id)).FirstOrDefaultAsync();
            if (RendezVous == null)
            {
                return NotFound();
            }
            return Ok(RendezVous);
        }

        [HttpPost] 
        public async Task<ActionResult<RendezVous>> CreateRendezVous(RendezVous RendezVous)
        {
            try
            {
                RendezVous ajoutRendezVous = new RendezVous
                {
                    PatientId = RendezVous.PatientId,
                    MedecinId = RendezVous.MedecinId,
                    DateDebut = RendezVous.DateDebut,
                    DateFin = RendezVous.DateFin,
                    InfosComplementaires = RendezVous.InfosComplementaires
                };

                _RendezVousContext.RendezVous.Add(RendezVous);
                await _RendezVousContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRendezVousById), new { id = RendezVous.IdRendezVous }, RendezVous);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du salarié : {ex.Message}");
                return StatusCode(500, $"Une erreur s'est produite lors de la création du salarié : {ex.Message}");
            }
        }



        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteRendezVous(int id)
        {
            var RendezVous = await _RendezVousContext.RendezVous.FindAsync(id);
            if (RendezVous == null)
            {
                return NotFound();
            }
            _RendezVousContext.RendezVous.Remove(RendezVous);
            await _RendezVousContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")] 
        public async Task<IActionResult> UpdateRendezVous(int id, RendezVous RendezVous)
        {
            if (!id.Equals(RendezVous.IdRendezVous))
            {
                return BadRequest("ID's are different");
            }
            var RendezVousToUpdate = await _RendezVousContext.RendezVous.FindAsync(id);
            if (RendezVousToUpdate == null)
            {
                return NotFound($"RendezVous with Id ={id} not found");
            }
            RendezVousToUpdate.PatientId = RendezVous.PatientId;
            RendezVousToUpdate.MedecinId = RendezVous.MedecinId;
            RendezVousToUpdate.DateDebut = RendezVous.DateDebut;
            RendezVousToUpdate.DateFin = RendezVous.DateFin;
            RendezVousToUpdate.InfosComplementaires = RendezVous.InfosComplementaires;
            await _RendezVousContext.SaveChangesAsync();
            return NoContent();
        }
       
            
        
    }
}
