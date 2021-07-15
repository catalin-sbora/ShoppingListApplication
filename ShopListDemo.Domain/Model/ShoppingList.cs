
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Domain.Model
{
    public class ShoppingList
    {
        private List<ShoppingListItem> _items = new List<ShoppingListItem>();
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; private set; }
        public IReadOnlyList<ShoppingListItem> Items => _items.OrderBy(item=>item.Rank).ToList().AsReadOnly();
        public int ItemsCount { get; private set; }
        private ShoppingList() { }
        public static ShoppingList Create(string listName)
        {
            if (string.IsNullOrEmpty(listName))
                throw new ArgumentException("The name for the list must be different than null or empty.", "listName");

            return new ShoppingList
            {
                Id = Guid.NewGuid().ToString(),
                Name = listName,
                CreateTime = DateTime.UtcNow                
            };
        }

        public ShoppingListItem AddItem(string name, int quantity, string measuringUnit, int rank)
        {
            var item = ShoppingListItem.Create(name, quantity, measuringUnit, rank);
            _items.Add(item);
            ItemsCount = _items.Count();
            return item;
        }
        
        public int RemoveItem(string itemId)
        {
           var foundItem = _items.SingleOrDefault(item => item.Id.Equals(itemId));
            if (foundItem == default(ShoppingListItem))
            {
                return 0;
            }

            _items.Remove(foundItem);
            ItemsCount = _items.Count();
            return 1;
        }
        

        public ShoppingListItem GetItemById(string itemId)
        {
            var foundItem = _items.SingleOrDefault(item => item.Id.Equals(itemId));           
            return foundItem;
        }

        public ShoppingListItem RankUpItem(string itemId)
        {
            var foundItem = _items.SingleOrDefault(item => item.Id.Equals(itemId));
            if (foundItem == default(ShoppingListItem))
            {
                return foundItem;
            }

            var sortedItems = _items.OrderBy(item => item.Rank)
                                    .ToList();
            ShoppingListItem item = null;
            for (int i = 0; i < sortedItems.Count; i++)
            {
                item = sortedItems.ElementAt(i);

                if (item.Id.Equals(itemId))
                {
                    if (i != 0)
                    {
                        item.Rank--;
                        sortedItems.ElementAt(i - 1)
                                   .Rank++;
                    }
                    break;
                }
            }
            return item;
        }

        public ShoppingListItem RankDownItem(string itemId)
        {
            var foundItem = _items.SingleOrDefault(item => item.Id.Equals(itemId));
            if (foundItem == default(ShoppingListItem))
            {
                return foundItem;
            }
            var sortedItems = _items.OrderBy(item => item.Rank)
                                    .ToList();
            ShoppingListItem item = null;
            for (int i = 0; i < sortedItems.Count; i++)
            {
                item = sortedItems.ElementAt(i);

                if (item.Id.Equals(itemId))
                {
                    if (i < (sortedItems.Count - 1))
                    {
                        item.Rank++;
                        sortedItems.ElementAt(i + 1)
                                   .Rank--;
                    }
                    break;
                }
            }
            return item;
        }
    }
}
