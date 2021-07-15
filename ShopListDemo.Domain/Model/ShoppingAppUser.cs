
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Domain.Model
{
    public class ShoppingAppUser
    {
        private List<ShoppingList> _shoppingLists =  new List<ShoppingList>();
        public string Id { get; private set; }
        public string ExternalId { get; private set; }

        public IReadOnlyList<ShoppingList> ShoppingLists  => _shoppingLists.AsReadOnly(); 

        private ShoppingAppUser() { }
        public static ShoppingAppUser Create(string externalId)
        {
            var retVal = new ShoppingAppUser 
            {
                Id = Guid.NewGuid().ToString(),
                ExternalId = externalId
               
            };

            return retVal;
        }

        public ShoppingList CreateShoppingList(string shoppingListName)
        {
            var newList = ShoppingList.Create(shoppingListName);
            _shoppingLists.Add(newList);
            return newList;
        }

        public int RemoveShoppingList(string shoppingListId)
        {            
            return _shoppingLists.RemoveAll(list => list.Id.Equals(shoppingListId));            
        }

        public ShoppingList UpdateShoppingListName(string listId, string newName)
        {
            if (string.IsNullOrEmpty(newName))
            {
                throw new ArgumentException($"The name of the shopping list {listId} must be different than null or empty value", "newName");
            }

            var list = _shoppingLists.Where(list => list.Id.Equals(listId))
                                    .SingleOrDefault();

            if (list != default(ShoppingList))
            {
                list.Name = newName;
            }           

            return list;
        }


        
    }
}
