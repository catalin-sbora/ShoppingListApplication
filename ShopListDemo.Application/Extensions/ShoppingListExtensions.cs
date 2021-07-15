using ShopListDemo.Domain.Model;
using ShopListDemo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Application.Extensions
{
    public static class ShoppingListExtensions
    {
        public static ShoppingListData ToData(this ShoppingList shoppingList) 
        {
            var shoppingListData = new ShoppingListData
            {
                Id = shoppingList.Id,
                CreateTime = shoppingList.CreateTime,
                Name = shoppingList.Name,
                Items = shoppingList.Items
                                    .Select(item =>
                                                    new ShoppingListItemData
                                                    {
                                                        Id = item.Id,
                                                        IsChecked = item.IsChecked,
                                                        MeasuringUnit = item.MeasuringUnit,
                                                        Name = item.Name,
                                                        Quantity = item.Quantity
                                                    })
                                    .ToList(),
                ItemsCount = shoppingList.Items.Count()

            };

            return shoppingListData;

        }

        public static ShoppingListItemData ToData(this ShoppingListItem shoppingListItem)
        {
            var shoppingListItemData = new ShoppingListItemData
            {
                Id = shoppingListItem.Id,
                Name = shoppingListItem.Name,
                Quantity = shoppingListItem.Quantity,
                MeasuringUnit = shoppingListItem.MeasuringUnit,
                IsChecked = shoppingListItem.IsChecked,
                Rank = shoppingListItem.Rank

            };

            return shoppingListItemData;

        }
    }
}
