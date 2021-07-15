using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Shared.Data
{
    public class ShoppingListData
    {
        public string Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public List<ShoppingListItemData> Items { get; set; }
        public int ItemsCount { get; set; }

    }
}
