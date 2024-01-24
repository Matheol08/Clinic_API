using Microsoft.EntityFrameworkCore;
using ModelsAdmin;
using ModelsSalarie;
using ModelsService;
using ModelsSite;
using WpfApp08.Models2;
namespace API_rest.Contexts
{
    public class AnnuaireContext : DbContext
    {

        public AnnuaireContext(DbContextOptions<AnnuaireContext> options)
            : base(options)
        {
        
        }

        
        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<Medecins> Medecins { get; set; }
        public DbSet<Specialites> Specialites { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Administrateur> Administrateur { get; set; }

    }
   
}