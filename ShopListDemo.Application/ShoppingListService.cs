using ShopListDemo.Application.Exceptions;
using ShopListDemo.Application.Extensions;
using ShopListDemo.Domain.Abstractions;
using ShopListDemo.Domain.Model;
using ShopListDemo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Application
{
    public class ShoppingListService
    {
        private readonly IPersistenceContext persistenceContext;
        private readonly IUserRepository userRepo;
        private readonly string userId;

        private async Task<ShoppingList> GetShoppingListById(string listId)
        {
            var user = await userRepo.GetByExternalId(userId);
            if (user == default(ShoppingAppUser))
            {
                throw new UserDoesNotExistException(userId);
            }

            var shoppingList = user.ShoppingLists
                           .Where(list => list.Id.Equals(listId, StringComparison.OrdinalIgnoreCase))
                           .SingleOrDefault();

            if (shoppingList == default(ShoppingList))
            {
                throw new ListDoesNotExistException(userId, listId);
            }

            return shoppingList;
        }
        public ShoppingListService(IPersistenceContext persistenceContext, string userId)
        {
            this.persistenceContext = persistenceContext;
            userRepo = persistenceContext.GetUserRepository();
            this.userId = userId;
        }

        public async Task<ShoppingListData> GetShoppingList(string shoppingListId)
        {

            var shoppingList = await GetShoppingListById(shoppingListId);
            return shoppingList.ToData();
        }

        public async Task<ShoppingListItemData> AddListItem(string shoppingListId, ShoppingListItemData item)
        {
            var selectedList = await GetShoppingListById(shoppingListId);
            var addedItem = selectedList.AddItem(item.Name, item.Quantity, item.MeasuringUnit, item.Rank);
            await persistenceContext.SaveChangesAsync();
            return addedItem.ToData();
        }

        public async Task RemoveListItem(string listId, string shoppingListItemId)
        {
            var selectedList = await GetShoppingListById(listId);
            if (selectedList.RemoveItem(shoppingListItemId) == 0)
            {
                throw new ItemNotInTheListException(shoppingListItemId);
            }
            await persistenceContext.SaveChangesAsync();
        }

        public async Task<ShoppingListItemData> UpdateListItem(string listId, ShoppingListItemData newData)
        {
            var selectedList = await GetShoppingListById(listId);
            var item = selectedList.GetItemById(newData.Id);
            if (item == default(ShoppingListItem))
            {
                throw new ItemNotInTheListException(newData.Id);
            }
            item.IsChecked = newData.IsChecked;
            item.MeasuringUnit = newData.MeasuringUnit;
            item.Name = newData.Name;
            item.Quantity = newData.Quantity;
            item.Rank = newData.Rank;

            await persistenceContext.SaveChangesAsync();
            return item.ToData();
        }

        public async Task<ShoppingListItemData> RankUpItem(string listId, string itemId)
        {
            var selectedList = await GetShoppingListById(listId);
            var item = selectedList.RankUpItem(itemId)
                               .ToData();
            await persistenceContext.SaveChangesAsync();
            return item;
        }

        public async Task<ShoppingListItemData> RankDownItem(string listId, string itemId)
        {
            var selectedList = await GetShoppingListById(listId);
            var item =  selectedList.RankDownItem(itemId)
                               .ToData();
            await persistenceContext.SaveChangesAsync();
            return item;
        }
    }
}
