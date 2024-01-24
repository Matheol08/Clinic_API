using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsSite;
using ModelsService;
using System.Security.Policy;

namespace ModelsSalarie
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