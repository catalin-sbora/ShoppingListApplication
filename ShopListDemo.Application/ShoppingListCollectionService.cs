using ShopListDemo.Application.Exceptions;
using ShopListDemo.Application.Extensions;
using ShopListDemo.Domain.Abstractions;
using ShopListDemo.Domain.Model;
using ShopListDemo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListDemo.Application
{
    public class ShoppingListCollectionService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IUserRepository userRepo;
        //private readonly ShoppingAppUser user;
        private readonly string userId;

        private async Task<ShoppingAppUser> GetCurrentUser()
        {
            var user = await userRepo.GetByExternalId(userId);
            if (user == default(ShoppingAppUser))
            {
                throw new UserDoesNotExistException(userId);
            }
            return user;
        }
        public ShoppingListCollectionService(IPersistenceContext persistenceContext, string userId)
        {
            this.persistenceContext = persistenceContext;
            userRepo = persistenceContext.GetUserRepository();
            this.userId = userId;            
        }
        public async Task<List<ShoppingListData>> GetShoppingListCollection() 
        {            
            var retList = new List<ShoppingListData>();
            var user = await GetCurrentUser();
            foreach (var list in user.ShoppingLists)
            {
                retList.Add(list.ToData());   
            }
            return retList;
        }       

        public async Task<ShoppingListData> CreateShoppingList(string shoppingListName)
        {
            var user = await GetCurrentUser();
            var shoppingList = user.CreateShoppingList(shoppingListName);
            await persistenceContext.SaveChangesAsync();
            return shoppingList.ToData();
        }

        public async Task RemoveShoppingList(string shoppingListId)
        {
            var user = await GetCurrentUser();
            if (user.RemoveShoppingList(shoppingListId) == 0)
            {
                throw new ListDoesNotExistException(userId, shoppingListId);
            }

            await persistenceContext.SaveChangesAsync();
        }

        public async Task<ShoppingListData> UpdateShoppingList(ShoppingListData shoppingList)
        {
            var user = await GetCurrentUser();
            var existingShoppingList = user.UpdateShoppingListName(shoppingList.Id, shoppingList.Name);
            await persistenceContext.SaveChangesAsync();

            return existingShoppingList.ToData();
        }


    }
}
