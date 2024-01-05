
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_rest.Contexts;
using ModelsSite;
using ModelsSalarie;
namespace SiteContrôleur
{
    [ApiController]
    [Route("api/sites")]

    public class SiteContrôleur1 : ControllerBase
    {


        private readonly AnnuaireContext _Sitecontext;

        public SiteContrôleur1(AnnuaireContext context)
        {
            _Sitecontext = context;

        }

       

        [HttpGet] //Get Site
        public async Task<ActionResult<IEnumerable<Sites>>> GetSites()
        {
            return await _Sitecontext.Sites.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sites>> GetSitesById(int ID)
        {
              var site = await _Sitecontext.Sites.Where(c => c.IDSite.Equals(ID)).FirstOrDefaultAsync();
            if (site ==null) 
            {
                return NotFound();
            }
            return Ok(site);
        }

        [HttpPost]//insert Site

        public async Task<ActionResult<Sites>> CreateSite(Sites site)
        {
            _Sitecontext.Sites.Add(site);
            await _Sitecontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSitesById), new { id = site.IDSite }, site);
        }

        [HttpDelete("{id}")]//Delete by ID

        public async Task<IActionResult> DeleteSite(int ID)
        {
            var site = await _Sitecontext.Sites.FindAsync(ID);   
            if (site == null)
            {
                return NotFound();
            }
            _Sitecontext.Sites.Remove(site);
            await _Sitecontext.SaveChangesAsync();
            return NoContent();
        }


        [HttpPut("{id}")] //Mettre à jour Site
        
        public async Task<IActionResult> UpdateSite(int ID, Sites site)
        {

            if (!ID.Equals(site.IDSite))
            {
                return BadRequest("ID's are different");   
            }
            var sitetoupdate = await _Sitecontext.Sites.FindAsync(ID);
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