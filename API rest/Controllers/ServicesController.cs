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
                private readonly AnnuaireContext _contextService_Employes;

                public ServiceEmployeController(AnnuaireContext context)
                {
                    _contextService_Employes = context;
                }

                [HttpGet] //get all Services
                public async Task<ActionResult<IEnumerable<Service_Employe>>> GetServiceEmployes()
                {
                    return await _contextService_Employes.Service_Employe.ToListAsync();
                }

                [HttpGet("{id}")]
                public async Task<ActionResult<Service_Employe>> GetServiceEmployeById(int ID)
                {
                    var serviceEmploye = await _contextService_Employes.Service_Employe.Where(c => c.IDService.Equals(ID)).FirstOrDefaultAsync();
                    if (serviceEmploye == null)
                    {
                        return NotFound();
                    }
                    return Ok(serviceEmploye);
                }

                [HttpPost] //insert un service
                public async Task<ActionResult<Service_Employe>> CreateServiceEmploye(Service_Employe serviceEmploye)
                {
                    _contextService_Employes.Service_Employe.Add(serviceEmploye);
                    await _contextService_Employes.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetServiceEmployeById), new { id = serviceEmploye.IDService }, serviceEmploye);
                }

                [HttpDelete("{id}")] //Delete by ID un service
                public async Task<IActionResult> DeleteServiceEmploye(int ID)
                {
                    var serviceEmploye = await _contextService_Employes.Service_Employe.FindAsync(ID);
                    if (serviceEmploye == null)
                    {
                        return NotFound();
                    }
                    _contextService_Employes.Service_Employe.Remove(serviceEmploye);
                    await _contextService_Employes.SaveChangesAsync();
                    return NoContent();
                }

                [HttpPut("{id}")] //mettre à jour Service
                public async Task<IActionResult> UpdateServiceEmploye(int id, Service_Employe serviceEmploye)
                {
                    if (!id.Equals(serviceEmploye.IDService))
                    {
                        return BadRequest("ID's are different");
                    }
                    var serviceEmployeToUpdate = await _contextService_Employes.Service_Employe.FindAsync(id);
                    if (serviceEmployeToUpdate == null)
                    {
                        return NotFound($"Service Employe with Id ={id} not found");
                    }
                    serviceEmployeToUpdate.Nom_Service = serviceEmploye.Nom_Service;
                    await _contextService_Employes.SaveChangesAsync();
                    return NoContent();
                }
            }
        }
