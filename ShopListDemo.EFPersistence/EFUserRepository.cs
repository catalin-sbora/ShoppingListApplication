using Microsoft.EntityFrameworkCore;
using ShopListDemo.Domain.Abstractions;
using ShopListDemo.Domain.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListDemo.EFPersistence
{
    public class EFUserRepository: IUserRepository
    {
        private readonly ShoppingListDbContext dbContext;


        public EFUserRepository(ShoppingListDbContext context)
        {
            dbContext = context;
        }      

        public async Task<ShoppingAppUser> GetByExternalId(string externalId)
        {
            return await dbContext.AppUsers
                                    .Where(user =>
                                            user.ExternalId
                                                .Equals(externalId))
                                    .SingleOrDefaultAsync();
        }

        public async Task<ShoppingAppUser> GetById(string id)
        {
            return await dbContext.AppUsers
                                    .Where(user =>
                                                user.Id
                                                    .Equals(id))
                                    .SingleOrDefaultAsync();
        }

        public async Task<ShoppingAppUser> Add(ShoppingAppUser appUser)
        {
            await dbContext.AppUsers
                            .AddAsync(appUser);
            return appUser;
        }
    }
}
