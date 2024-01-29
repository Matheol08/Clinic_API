using API_rest.Contexts;
using System.ComponentModel.DataAnnotations;

namespace ModelsSpecialites
{
    public class Specialites
    {
        [Required]
        [Key]
        public int IdSpecialite { get; set; }
        public string Libelle { get; set; }


    }

}
