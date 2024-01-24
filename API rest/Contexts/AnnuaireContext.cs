using Microsoft.EntityFrameworkCore;
using ModelsAdmin;
using ModelsRendezVous;
using ModelsMedecins;
using ModelsSpecialites;
using ModelsPatients;
namespace API_rest.Contexts
{
    public class ClinicContext : DbContext
    {

        public ClinicContext(DbContextOptions<ClinicContext> options)
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