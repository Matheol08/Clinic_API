using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsMedecins;

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
        public async Task<ActionResult<Medecins>> CreateMedecins(Medecins Medecins)
        {
            _contextMedecins.Medecins.Add(Medecins);
            await _contextMedecins.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMedecinsparId), new { id = Medecins.IdMedecin }, Medecins);
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
                public async Task<IActionResult> UpdateMedecins(int id, Medecins Medecins)
                {
                    if (!id.Equals(Medecins.IdMedecin))
                    {
                        return BadRequest("ID's are different");
                    }
                    var MedecinsToUpdate = await _contextMedecins.Medecins.FindAsync(id);
                    if (MedecinsToUpdate == null)
                    {
                        return NotFound($"Service Employe with Id ={id} not found");
                    }
                    MedecinsToUpdate.Nom = Medecins.Nom;
                    await _contextMedecins.SaveChangesAsync();
                    return NoContent();
                }
            }
        }
