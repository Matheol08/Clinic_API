using ModelsSalarie;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelsService
{
    public class Medecins
    {

        [Required]
        [Key]

        public int IdMedecin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [ForeignKey("Specialites")]
        public int SpecialiteId { get; set; }

    }

}
