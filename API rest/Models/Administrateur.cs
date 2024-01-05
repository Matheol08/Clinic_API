using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
namespace ModelsAdmin
{
    public class Administrateur
    {
        [Required]
        [Key]
        public int idadmin { get; set; }
        public string AdminMDP { get; set; }
    

    }
}