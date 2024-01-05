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

        
        public DbSet<Salaries> Salaries { get; set; }
        public DbSet<Service_Employe> Service_Employe{ get; set; }
        public DbSet<Sites> Sites { get; set; }

        public DbSet<Administrateur> Administrateur { get; set; }

    }
   
}