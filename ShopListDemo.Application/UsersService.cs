using ShopListDemo.Domain.Abstractions;
using ShopListDemo.Domain.Model;
using System.Threading.Tasks;

namespace ShopListDemo.Application
{
    public class UsersService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IUserRepository userRepository;

        public UsersService(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
            if (persistenceContext != null)
            { 
                userRepository = persistenceContext.GetUserRepository();
            }
        }

        public async Task<ShoppingAppUser> RegisterUser(string externalId)
        {
            var user = ShoppingAppUser.Create(externalId);
            var retUser = await userRepository.Add(user);
            await persistenceContext.SaveChangesAsync();
            return retUser;
        }
    }
}
