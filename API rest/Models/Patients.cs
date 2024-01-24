using System.ComponentModel.DataAnnotations;

namespace ModelsPatients
{
    public class Patients
    {
        [Required]
        [Key]
        public int IdPatient { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
    }
}
