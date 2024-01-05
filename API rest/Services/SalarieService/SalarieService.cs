//using API_rest.Contexts;
//using Microsoft.EntityFrameworkCore;
//using ModelsSalarie;
//using InterfaceSalarieService;

//namespace ServiceSalarie
//{
//    public class SalarieService : ISalarieService   
//    {
//        private readonly AnnuaireContext _dbContext;

//        public SalarieService(AnnuaireContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<List<Salaries>> GetSalaries(int id)
//        {
//            var salaries = await _dbContext.Salaries.Where(s => s.ID == id).ToListAsync();

//            return salaries;
//        }



//    }
//}

