using Microsoft.AspNetCore.Components;
using ShopListDemo.Client.Components.DeleteDialogComponent;
using ShopListDemo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListDemo.Client.Components.SavedListsComponent
{
    public class SavedListsEntryBase: ComponentBase
    {
        [Parameter]
        public string BackgroundClass { get; set; }

        [Parameter]
        public ShoppingListData EntryModel { get; set; }

        [Parameter]
        public EventCallback<ShoppingListData> OnDelete { get; set; }

        [Parameter]
        public EventCallback<ShoppingListData> OnAddItem { get; set; }

        [Parameter]
        public bool IsEditor { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected DeleteDialog deleteDialog;

        protected void OpenShoppingList()
        {
            NavigationManager.NavigateTo($"/list/{EntryModel.Id}");
        }
    }
}
