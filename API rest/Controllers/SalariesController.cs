using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using ModelsSalarie;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Azure;

namespace SalarieContrôleur
{
    [ApiController]
    [Route("api/salaries")]
    public class SalariesController : ControllerBase
    {
        private readonly AnnuaireContext _SalarieContext;

        public SalariesController(AnnuaireContext context)
        {
            _SalarieContext = context;
        }

        [HttpGet]//récupère tous les salaries
            public async Task<ActionResult<IEnumerable<Medecins>>> GetSalaries()
        {
            var salariesWithSitesAndService = await _SalarieContext.Salaries
                .Include(s => s.Sites)
                .Include(s => s.Service_Employe)
                .ToListAsync();

            return Ok(salariesWithSitesAndService);
        }


        [HttpGet("RechercheNometPrenom")] //Search by Name et FirstName
        public async Task<ActionResult<IEnumerable<Medecins>>> GetSalariesBySearchTerm(string searchTerm)
        {
            IQueryable<v> query = _SalarieContext.Salaries;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(s => EF.Functions.Like(s.Nom, searchTerm + "%") || EF.Functions.Like(s.Prenom, searchTerm + "%"));
            }

            var result = await query.Include(s => s.Sites).Include(s => s.Service_Employe).ToListAsync();

            return Ok(result);
        }


        [HttpGet("rechercheSite")]//recherche par site
        public async Task<ActionResult<IEnumerable<v>>> GetSalariesBySite(string ville)
        {
            IQueryable<Medecins> query = _SalarieContext.Salaries;

            
            if (!string.IsNullOrEmpty(ville))
            {
                query = query.Where(s => s.Sites.Ville == ville);
            }

            

               var result = await query.Include(s => s.Sites).Include(s => s.Service_Employe).ToListAsync();

            return Ok(result);
        }
        [HttpGet("rechercheSiteEtService")] //recherche de salarié par Site et Service
        public async Task<ActionResult<IEnumerable<Medecins>>> GetSalariesBySiteAndService(string ville, string nomService)
        {
            IQueryable<Medecins> query = _SalarieContext.Salaries;

            if (!string.IsNullOrEmpty(ville))
            {
                query = query.Where(s => s.Sites.Ville == ville);
            }

            if (!string.IsNullOrEmpty(nomService))
            {
                query = query.Where(s => s.Service_Employe.Nom_Service == nomService);
            }

            var result = await query.Include(s => s.Sites).Include(s => s.Service_Employe).ToListAsync();

            return Ok(result);
        }



        [HttpGet("rechercheService")] //Recherche par Service
        public async Task<ActionResult<IEnumerable<Medecins>>> GetSalariesByService(string Nom_Service)
        {
            IQueryable<Medecins> query = _SalarieContext.Salaries;


            if (!string.IsNullOrEmpty(Nom_Service))
            {
                query = query.Where(s => s.Service_Employe.Nom_Service == Nom_Service);
            }



            var result = await query.Include(s => s.Sites).Include(s => s.Service_Employe).ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")] //recherche by id Salarie
        public async Task<ActionResult<Medecins>> GetSalarieById(int ID)
        {
            var salarie = await _SalarieContext.Salaries.Where(c => c.IdMedecin.Equals(ID)).FirstOrDefaultAsync();
            if (salarie == null)
            {
                return NotFound();
            }
            return Ok(salarie);
        }

        [HttpPost] //insert salarie
        public async Task<ActionResult<Medecins>> CreateSalarie(Ajout_Salaries ajoutSalarie)
        {
            try
            {
                Medecins salarie = new Medecins
                {
                    Nom = ajoutSalarie.Nom,
                    Prenom = ajoutSalarie.Prenom,
                    Email = ajoutSalarie.Email,
                    Telephone_portable = ajoutSalarie.Telephone_portable,
                    Telephone_fixe = ajoutSalarie.Telephone_fixe,
                    IDSite = ajoutSalarie.IDSite,
                    IDService = ajoutSalarie.IDService
                };

                _SalarieContext.Salaries.Add(salarie);
                await _SalarieContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetSalarieById), new { id = salarie.IdMedecin }, salarie);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la création du salarié : {ex.Message}");
                return StatusCode(500, $"Une erreur s'est produite lors de la création du salarié : {ex.Message}");
            }
        }



        [HttpDelete("{id}")] //Delete by ID
        public async Task<IActionResult> DeleteSalarie(int ID)
        {
            var salarie = await _SalarieContext.Salaries.FindAsync(ID);
            if (salarie == null)
            {
                return NotFound();
            }
            _SalarieContext.Salaries.Remove(salarie);
            await _SalarieContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")] //Mettre à jour by ID
        public async Task<IActionResult> UpdateSalarie(int ID, Medecins salarie)
        {
            if (!ID.Equals(salarie.IDSalaries))
            {
                return BadRequest("ID's are different");
            }
            var salarieToUpdate = await _SalarieContext.Salaries.FindAsync(ID);
            if (salarieToUpdate == null)
            {
                return NotFound($"Salarie with Id ={ID} not found");
            }
            salarieToUpdate.Nom = salarie.Nom;
            salarieToUpdate.Prenom = salarie.Prenom;
            salarieToUpdate.Telephone_fixe = salarie.Telephone_fixe;
            salarieToUpdate.Telephone_portable = salarie.Telephone_portable;
            salarieToUpdate.Email = salarie.Email;
            salarieToUpdate.IDService = salarie.IDService;
            salarieToUpdate.IDSite = salarie.IDSite;
            await _SalarieContext.SaveChangesAsync();
            return NoContent();
        }
       
            
        
    }
}
