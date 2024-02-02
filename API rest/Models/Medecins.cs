
using ModelsSpecialites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace ModelsMedecins
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
        [JsonIgnore]
        public virtual Specialites Specialites { get; set; }


    }
    public class AjoutMedecins
    {

        [Required]
        [Key]

        public int IdMedecin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [ForeignKey("Specialites")]
        public int IdSpecialite { get; set; }
       // public virtual Specialites Specialites { get; set; }


    }
    public class majMedecins
    {

        [Required]
        [Key]

        public int IdMedecin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        [ForeignKey("Specialites")]
        public int SpecialiteId { get; set; }
       // [JsonIgnore]
        //public virtual Specialites Specialites { get; set; }

    }
}
