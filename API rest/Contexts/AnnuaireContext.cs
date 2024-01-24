using Microsoft.EntityFrameworkCore;
using ModelsAdmin;
using ModelsSalarie;
using ModelsService;
using ModelsSite;
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

        public DbSet<Administrateur> Administrateur { get; set; }

    }
   
}