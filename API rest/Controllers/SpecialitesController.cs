
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_rest.Contexts;
using ModelsSpecialites;
using ModelsRendezVous;
namespace SiteContrôleur
{
    [ApiController]
    [Route("api/Specialites")]

    public class SpecialitesContrôller1 : ControllerBase
    {


        private readonly ClinicContext _SpecialitesContext;

        public SpecialitesContrôller1(ClinicContext context)
        {
            _SpecialitesContext = context;

        }

       

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Specialites>>> GetSpecialites()
        {
            return await _SpecialitesContext.Specialites.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Specialites>> GetSpecialitesById(int ID)
        {
              var Specialites = await _SpecialitesContext.Specialites.Where(c => c.IdSpecialite.Equals(ID)).FirstOrDefaultAsync();
            if (Specialites == null) 
            {
                return NotFound();
            }
            return Ok(Specialites);
        }

        [HttpPost]

        public async Task<ActionResult<Specialites>> CreateSpecialites(Specialites Specialites)
        {
            _SpecialitesContext.Specialites.Add(Specialites);
            await _SpecialitesContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSpecialitesById), new { id = Specialites.IdSpecialite }, Specialites);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSpecialites(int ID)
        {
            var Specialites = await _SpecialitesContext.Specialites.FindAsync(ID);   
            if (Specialites == null)
            {
                return NotFound();
            }
            _SpecialitesContext.Specialites.Remove(Specialites);
            await _SpecialitesContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")] 
        
        public async Task<IActionResult> UpdateSite(int ID, Specialites Specialites)
        {

            if (!ID.Equals(Specialites.IdSpecialite))
            {
                return BadRequest("ID's are different");   
            }
            var Specialitestoupdate = await _SpecialitesContext.Specialites.FindAsync(ID);
            if (Specialitestoupdate == null)
            {
                return NotFound($"Site with Id ={ID} not found");
            }
            Specialitestoupdate.Libelle = Specialites.Libelle;
            await _SpecialitesContext.SaveChangesAsync();
            return NoContent();
        }

        
    }
}