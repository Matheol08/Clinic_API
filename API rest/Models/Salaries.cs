using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ModelsSite;
using ModelsService;
using System.Security.Policy;
using Newtonsoft.Json;

namespace ModelsSalarie
{
    public class Salaries
    {


        [Required][Key]
        public int IDSalaries { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone_fixe { get; set; }
        public string Telephone_portable { get; set; }
        public string Email { get; set; }

        [ForeignKey("Service_Employe")]
        public int IDService { get; set; }

        public virtual Service_Employe Service_Employe { get; set; }
        
        [ForeignKey("Sites")]
        public int IDSite { get; set; }

        public virtual Sites Sites { get; set; }

     
    }
    public class Ajout_Salaries
    {
        
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone_fixe { get; set; }
        public string Telephone_portable { get; set; }
        public string Email { get; set; }
        [ForeignKey("Service_Employe")]
        public int IDService { get; set; }


        [ForeignKey("Sites")]
        public int IDSite { get; set; }

    }
    
    
}