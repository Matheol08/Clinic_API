using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsMedecins;

    namespace API_rest.Contrôleurs.Service
    {
        [ApiController]
        [Route("api/services")]

            public class ServiceEmployeController : ControllerBase
            {
                private readonly ClinicContext _contextMedecinss;

                public ServiceEmployeController(ClinicContext context)
                {
                    _contextMedecinss = context;
                }

                [HttpGet] 
                public async Task<ActionResult<IEnumerable<Medecins>>> GetServiceEmployes()
                {
                    return await _contextMedecinss.Medecins.ToListAsync();
                }

                [HttpGet("{id}")]
                public async Task<ActionResult<Medecins>> GetServiceEmployeById(int ID)
                {
                    var serviceEmploye = await _contextMedecinss.Medecins.Where(c => c.IdMedecin.Equals(ID)).FirstOrDefaultAsync();
                    if (serviceEmploye == null)
                    {
                        return NotFound();
                    }
                    return Ok(serviceEmploye);
                }

                [HttpPost] 
                public async Task<ActionResult<Medecins>> CreateServiceEmploye(Medecins Medecins)
                {
                    _contextMedecinss.Medecins.Add(Medecins);
                    await _contextMedecinss.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetServiceEmployeById), new { id = Medecins.IdMedecin }, Medecins);
                }

                [HttpDelete("{id}")]
                public async Task<IActionResult> DeleteMedecins(int ID)
                {
                    var Medecins = await _contextMedecinss.Medecins.FindAsync(ID);
                    if (Medecins == null)
                    {
                        return NotFound();
                    }
                    _contextMedecinss.Medecins.Remove(Medecins);
                    await _contextMedecinss.SaveChangesAsync();
                    return NoContent();
                }

                [HttpPut("{id}")] 
                public async Task<IActionResult> UpdateMedecins(int id, Medecins serviceEmploye)
                {
                    if (!id.Equals(serviceEmploye.IdMedecin))
                    {
                        return BadRequest("ID's are different");
                    }
                    var serviceEmployeToUpdate = await _contextMedecinss.Medecins.FindAsync(id);
                    if (serviceEmployeToUpdate == null)
                    {
                        return NotFound($"Service Employe with Id ={id} not found");
                    }
                    serviceEmployeToUpdate.Nom = serviceEmploye.Nom;
                    await _contextMedecinss.SaveChangesAsync();
                    return NoContent();
                }
            }
        }
