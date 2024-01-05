//using API_rest.Contexts;
//using ModelsService;
//using InterfaceServiceService;
//using Microsoft.EntityFrameworkCore;

//namespace ServiceService

//{
  
//    public class ServiceService : IServiceService
//    {
//        private readonly AnnuaireContext _dbContext;

//        public ServiceService(AnnuaireContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<List<Service_Employe>> GetAllServices()
//        {
//            var services = await _dbContext.Service_Employe.ToListAsync();

//            return services;
//        }
//    }
//}