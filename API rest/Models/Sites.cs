using API_rest.Contexts;
using ModelsSalarie;
using System.ComponentModel.DataAnnotations;

namespace ModelsSite
{
    public class Sites
    {
        [Required][Key]
        public int IDSite { get; set; }
        public string Ville { get; set; }
        public string Statut_Site { get; set; }
      
    }

}
