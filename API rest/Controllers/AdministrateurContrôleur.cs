using API_rest.Contexts;
using Microsoft.AspNetCore.Mvc;
namespace API_rest.Controllers
{
    [ApiController]
    [Route("api/Admin")]

    public class AdminController : ControllerBase
    {
        private readonly ClinicContext _Admin_Employes;

        public AdminController(ClinicContext context)
        {
            _Admin_Employes = context;
        }
        [HttpGet("verificationAdmin")] 
        public async Task<ActionResult<string>> VerificationAdmin([FromQuery] int idadmin, [FromQuery] string valeurSaisie)
        {
            try
            {
                var administrateur = await _Admin_Employes.Administrateur.FindAsync(idadmin);

                if (administrateur == null)
                {
                    return NotFound("Administrateur non trouvé");
                }

                string motDePasseStocke = administrateur.AdminMDP;
                bool correspondance = (valeurSaisie == motDePasseStocke);

                if (correspondance)
                {
                    return Ok("Mot de passe correct.");
                }
                else
                {
                    return Ok("Mot de passe incorrect.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
            }
        }






    }
}