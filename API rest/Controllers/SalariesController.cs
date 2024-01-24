using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using ModelsRendezVous;
using ModelsMedecins;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure;
using ModelsPatients;

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


        [HttpGet("Date")]// Date
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVoussBySearchTerm(string searchTerm)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => EF.Functions.Like(s.Nom, searchTerm + "%") || EF.Functions.Like(s.Prenom, searchTerm + "%"));
            }

            var result = await query.Include(s => s.IdPatient).Include(s => s.MedecinId).ToListAsync();

            return Ok(result);
        }


        [HttpGet("recherchePatient")]
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVousByPatients(string NomPatient)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            
            if (!string.IsNullOrEmpty(NomPatient))
            {
                query = query.Where(s => s.Patients.Patients == NomPatient);
            }
            

                var result = await query.Include(s => s.IdPatient).Include(s => s.MedecinId).ToListAsync();

            return Ok(result);
        }
        [HttpGet("recherchePatientsEtMedecin)")] 
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVoussBySiteAndService(string Libelle, string nomMedecin)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;

            if (!string.IsNullOrEmpty(Libelle))
            {
                query = query.Where(s => s.IdPatient.Nom == Libelle);
            }

            if (!string.IsNullOrEmpty(nomMedecin))
            {
                query = query.Where(s => s.MedecinId.Nom == nomMedecin);
            }

            var result = await query.Include(s => s.Patients).Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }



        [HttpGet("rechercheMedecin")] //Recherche par Medecin
        public async Task<ActionResult<IEnumerable<RendezVous>>> GetRendezVoussByService(string Nom)
        {
            IQueryable<RendezVous> query = _RendezVousContext.RendezVous;


            if (!string.IsNullOrEmpty(Nom))
            {
                query = query.Where(s => s.IdMedecin.Nom == Nom);
            }



            var result = await query.Include(s => s.Specialites).Include(s => s.Medecins).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")] //recherche by id RendezVous
        public async Task<ActionResult<RendezVous>> GetRendezVousById(int ID)
        {
            var RendezVous = await _RendezVousContext.RendezVous.Where(c => c.IdRendezVous.Equals(ID)).FirstOrDefaultAsync();
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
                    IdPatient = RendezVous.IdPatient,
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



        [HttpDelete("{id}")] //Delete by ID
        public async Task<IActionResult> DeleteRendezVous(int ID)
        {
            var RendezVous = await _RendezVousContext.RendezVous.FindAsync(ID);
            if (RendezVous == null)
            {
                return NotFound();
            }
            _RendezVousContext.RendezVous.Remove(RendezVous);
            await _RendezVousContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")] //Mettre à jour by ID
        public async Task<IActionResult> UpdateRendezVous(int ID, RendezVous RendezVous)
        {
            if (!ID.Equals(RendezVous.IdRendezVous))
            {
                return BadRequest("ID's are different");
            }
            var RendezVousToUpdate = await _RendezVousContext.RendezVous.FindAsync(ID);
            if (RendezVousToUpdate == null)
            {
                return NotFound($"RendezVous with Id ={ID} not found");
            }
            RendezVousToUpdate.IdPatient = RendezVous.IdPatient;
            RendezVousToUpdate.MedecinId = RendezVous.MedecinId;
            RendezVousToUpdate.DateDebut = RendezVous.DateDebut;
            RendezVousToUpdate.DateFin = RendezVous.DateFin;
            RendezVousToUpdate.InfosComplementaires = RendezVous.InfosComplementaires;
            await _RendezVousContext.SaveChangesAsync();
            return NoContent();
        }
       
            
        
    }
}
