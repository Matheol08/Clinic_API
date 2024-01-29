
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_rest.Contexts;
using ModelsSpecialites;
using ModelsRendezVous;
namespace SpecialitesController
{
    [ApiController]
    [Route("api/Specialites")]

    public class SpecialitesContrôller : ControllerBase
    {


        private readonly ClinicContext _SpecialitesContext;

        public SpecialitesContrôller(ClinicContext context)
        {
            _SpecialitesContext = context;

        }

       

        [HttpGet] 
        public async Task<ActionResult<IEnumerable<Specialites>>> GetSpecialites()
        {
            return await _SpecialitesContext.Specialites.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Specialites>> GetSpecialitesByid(int id)
        {
              var Specialites = await _SpecialitesContext.Specialites.Where(c => c.IdSpecialite.Equals(id)).FirstOrDefaultAsync();
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
            return CreatedAtAction(nameof(GetSpecialitesByid), new { id = Specialites.IdSpecialite }, Specialites);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteSpecialites(int id)
        {
            var Specialites = await _SpecialitesContext.Specialites.FindAsync(id);   
            if (Specialites == null)
            {
                return NotFound();
            }
            _SpecialitesContext.Specialites.Remove(Specialites);
            await _SpecialitesContext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")] 
        
        public async Task<IActionResult> UpdateSite(int id, Specialites Specialites)
        {

            if (!id.Equals(Specialites.IdSpecialite))
            {
                return BadRequest("id's are different");   
            }
            var Specialitestoupdate = await _SpecialitesContext.Specialites.FindAsync(id);
            if (Specialitestoupdate == null)
            {
                return NotFound($"Site with id ={id} not found");
            }
            Specialitestoupdate.Libelle = Specialites.Libelle;
            await _SpecialitesContext.SaveChangesAsync();
            return NoContent();
        }

        
    }
}