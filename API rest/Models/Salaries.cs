using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsSite;
using ModelsService;
using System.Security.Policy;

namespace ModelsSalarie
{
    public class RendezVous
    {
        [Required]
        [Key]
        public int IdRendezVous { get; set; }
        [ForeignKey("IdPatient")]
        public int IdPatient { get; set; }
        [ForeignKey("IdMedecin")]

        public int MedecinId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string InfosComplementaires { get; set; }
    }


}