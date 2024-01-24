using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsService;

    namespace API_rest.Contrôleurs.Service
    {
        [ApiController]
        [Route("api/services")]

            public class ServiceEmployeController : ControllerBase
            {
                private readonly AnnuaireContext _contextMedecinss;

                public ServiceEmployeController(AnnuaireContext context)
                {
                    _contextMedecinss = context;
                }

                [HttpGet] //get all Services
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

                [HttpPost] //insert un service
                public async Task<ActionResult<Medecins>> CreateServiceEmploye(Medecins serviceEmploye)
                {
                    _contextMedecinss.Medecins.Add(serviceEmploye);
                    await _contextMedecinss.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetServiceEmployeById), new { id = serviceEmploye.IdMedecin }, serviceEmploye);
                }

                [HttpDelete("{id}")] //Delete by ID un service
                public async Task<IActionResult> DeleteServiceEmploye(int ID)
                {
                    var serviceEmploye = await _contextMedecinss.Medecins.FindAsync(ID);
                    if (serviceEmploye == null)
                    {
                        return NotFound();
                    }
                    _contextMedecinss.Medecins.Remove(serviceEmploye);
                    await _contextMedecinss.SaveChangesAsync();
                    return NoContent();
                }

                [HttpPut("{id}")] //mettre à jour Service
                public async Task<IActionResult> UpdateServiceEmploye(int id, Medecins serviceEmploye)
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
