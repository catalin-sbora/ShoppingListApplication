using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Domain.Model
{
    public class ShoppingListItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string MeasuringUnit { get; set; }
        public bool IsChecked { get; set; }

        public int Rank { get; set; }

        private ShoppingListItem() { }
        internal static ShoppingListItem Create(string name, int quantity, string measuringUnit, int rank)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("The 'name' parameter must be different than null or empty value.", "name");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("The 'quantity' parameter must be greater than 0", "quantity");
            }

            if (string.IsNullOrEmpty(measuringUnit))
            {
                throw new ArgumentException("The 'measuringUnit must be different than null or empty value'", "measuringUnit");
            }
            return new ShoppingListItem
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
                Quantity = quantity,
                MeasuringUnit = measuringUnit, 
                Rank = rank
            };
        }

    }
}
