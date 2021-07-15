using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Shared.Data
{
    public class ShoppingListItemData
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Quantity { get; set; }
        [Required]
        public string MeasuringUnit { get; set; } = "Piece";
        public bool IsChecked { get; set; }

        public int Rank { get; set; }

    }
}
