using Microsoft.Extensions.Configuration;
using ShopListDemo.Domain.Abstractions;
using System.Threading.Tasks;

namespace ShopListDemo.EFPersistence
{
    public class EFPersistenceContext : IPersistenceContext
    {
        private readonly ShoppingListDbContext context;
        private readonly IUserRepository usersRepo;
        
        public EFPersistenceContext(IConfiguration configuration)
        {
            EFPersistenceCosmosContextFactory factory = new EFPersistenceCosmosContextFactory(configuration);
            context = factory.CreateDbContext(null);
            usersRepo = new EFUserRepository(context);
        }
        public IUserRepository GetUserRepository()
        {
            return usersRepo;
        }

        public async Task InitializeAsync()
        {            
            await context.Database.EnsureCreatedAsync();            
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}
