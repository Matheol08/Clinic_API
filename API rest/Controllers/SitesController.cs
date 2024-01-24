
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_rest.Contexts;
using ModelsSite;
using ModelsSalarie;
namespace SiteContrôleur
{
    [ApiController]
    [Route("api/Specialites")]

    public class SiteContrôleur1 : ControllerBase
    {


        private readonly AnnuaireContext _Sitecontext;

        public SiteContrôleur1(AnnuaireContext context)
        {
            _Sitecontext = context;

        }

       

        [HttpGet] //Get Site
        public async Task<ActionResult<IEnumerable<Specialites>>> GetSpecialites()
        {
            return await _Sitecontext.Specialites.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Specialites>> GetSpecialitesById(int ID)
        {
              var site = await _Sitecontext.Specialites.Where(c => c.IdSpecialite.Equals(ID)).FirstOrDefaultAsync();
            if (site ==null) 
            {
                return NotFound();
            }
            return Ok(site);
        }

        [HttpPost]//insert Site

        public async Task<ActionResult<Specialites>> CreateSite(Specialites site)
        {
            _Sitecontext.Specialites.Add(site);
            await _Sitecontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSpecialitesById), new { id = site.IdSpecialite }, site);
        }

        [HttpDelete("{id}")]//Delete by ID

        public async Task<IActionResult> DeleteSite(int ID)
        {
            var site = await _Sitecontext.Specialites.FindAsync(ID);   
            if (site == null)
            {
                return NotFound();
            }
            _Sitecontext.Specialites.Remove(site);
            await _Sitecontext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")] //Mettre à jour Site
        
        public async Task<IActionResult> UpdateSite(int ID, Specialites site)
        {

            if (!ID.Equals(site.IdSpecialite))
            {
                return BadRequest("ID's are different");   
            }
            var sitetoupdate = await _Sitecontext.Specialites.FindAsync(ID);
            if (sitetoupdate == null)
            {
                return NotFound($"Site with Id ={ID} not found");
            }
            sitetoupdate.Statut_Site = site.Statut_Site;
            sitetoupdate.Ville = site.Ville;
            await _Sitecontext.SaveChangesAsync();
            return NoContent();
        }

        
    }
}