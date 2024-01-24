using API_rest.Contexts;
using ModelsSalarie;
using System.ComponentModel.DataAnnotations;

namespace ModelsSite
{
    public class Specialites
    {
        [Required]
        [Key]
        public int IdSpecialite { get; set; }
        public string Libelle { get; set; }


    }

}
