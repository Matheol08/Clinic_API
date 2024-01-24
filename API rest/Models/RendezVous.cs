using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsSpecialites;
using ModelsRendezVous;
using System.Security.Policy;

namespace ModelsRendezVous
{
    public class RendezVous
    {
        [Required]
        [Key]
        public int IdRendezVous { get; set; }
        [ForeignKey("Patients")]
        public int IdPatient { get; set; }
        [ForeignKey("Medecins")]

        public int MedecinId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string InfosComplementaires { get; set; }
    }


}