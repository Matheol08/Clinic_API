using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsPatients;

    namespace PatientsContrôller
{
        [ApiController]
        [Route("api/Patients")]

            public class PatientsController : ControllerBase
            {
                private readonly ClinicContext _contextPatients;

                public PatientsController(ClinicContext context)
                {
                    _contextPatients = context;
                }

                [HttpGet] 
                public async Task<ActionResult<IEnumerable<Patients>>> GetPatients()
                {
                    return await _contextPatients.Patients.ToListAsync();
                }

                [HttpGet("{id}")]
                public async Task<ActionResult<Patients>> GetPatientsById(int id)
                {
                    var Patients = await _contextPatients.Patients.Where(c => c.IdPatient.Equals(id)).FirstOrDefaultAsync();
                    if (Patients == null)
                    {
                        return NotFound();
                    }
                    return Ok(Patients);
                }

                [HttpPost] 
                public async Task<ActionResult<Patients>> CreatePatients(Patients Patients)
                {
                    _contextPatients.Patients.Add(Patients);
                    await _contextPatients.SaveChangesAsync();
                    return CreatedAtAction(nameof(GetPatientsById), new { id = Patients.IdPatient }, Patients);
                }

                [HttpDelete("{id}")] 
                public async Task<IActionResult> DeletePatients(int id)
                {
                    var Patients = await _contextPatients.Patients.FindAsync(id);
                    if (Patients == null)
                    {
                        return NotFound();
                    }
                    _contextPatients.Patients.Remove(Patients);
                    await _contextPatients.SaveChangesAsync();
                    return NoContent();
                }

                [HttpPut("{id}")]
                public async Task<IActionResult> UpdatePatient(int id, Patients Patients)
                {
                    if (!id.Equals(Patients.IdPatient))
                    {
                        return BadRequest("ID's are different");
                    }
                    var PatientsToUpdate = await _contextPatients.Patients.FindAsync(id);
                    if (PatientsToUpdate == null)
                    {
                        return NotFound($"Patients with Id ={id} not found");
                    }
            PatientsToUpdate.Nom = PatientsToUpdate.Nom;
                    await _contextPatients.SaveChangesAsync();
                    return NoContent();
                }
            }
        }
