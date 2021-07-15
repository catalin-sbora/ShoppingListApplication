using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopListDemo.Client.Components.DeleteDialogComponent
{
    public class DeleteDialogBase : ComponentBase
    {
        [Parameter]
        public string ItemDescription { get; set; }

        [Parameter]
        public EventCallback OnDeleteAccepted { get; set; }
        protected bool ShowConfirmation { get; set; }
        public void Show() 
        {
            ShowConfirmation = true;
            StateHasChanged();
        }

        protected void Close()
        {
            ShowConfirmation = false;
            StateHasChanged();
        }
    }
}
