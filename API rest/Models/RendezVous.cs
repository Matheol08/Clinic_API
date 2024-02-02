using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsSpecialites;
using ModelsRendezVous;
using System.Security.Policy;
using ModelsPatients;
using ModelsMedecins;

namespace ModelsRendezVous
{
    public class RendezVous
    {
        [Required]
        [Key]
        public int IdRendezVous { get; set; }
        [ForeignKey("Patients")]
        public int PatientId { get; set; }
        public virtual Patients Patients { get; set; }

        [ForeignKey("Medecins")]

        public int MedecinId { get; set; }
        public virtual Medecins Medecins { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string InfosComplementaires { get; set; }
    }
    public class ajoutRendezVous
    {
        public int IdRendezVous { get; set; }
        [ForeignKey("Patients")]
        public int PatientId { get; set; }

        [ForeignKey("Medecins")]

        public int MedecinId { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public string InfosComplementaires { get; set; }
    }

}