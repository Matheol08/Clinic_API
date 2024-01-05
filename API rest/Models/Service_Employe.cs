using ModelsSalarie;
using System.ComponentModel.DataAnnotations;

namespace ModelsService
{
    public class Service_Employe
    {
        [Required][Key]
        public int IDService { get; set; }
        public string Nom_Service { get; set; }

  
    }

}
