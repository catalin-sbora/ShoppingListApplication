using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Application.Exceptions
{
    public class ItemNotInTheListException:Exception
    {
        public string ItemId { get; private set; }
        public ItemNotInTheListException(string itemId) : base($"The item with the provided identifier: {itemId} cannot be found in the shopping list")
        {
            ItemId = itemId;
        }
    }
}
