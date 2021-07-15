using Newtonsoft.Json;
using ShopListDemo.Shared.Data;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ShopListDemo.Client.API
{
    public class ShoppingListClient
    {
        private readonly HttpClient httpClient;
        public ShoppingListClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ShoppingListData[]> GetShoppingLists(CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ShoppingListCollection");
            var response = await httpClient.SendAsync(request);
            await response.EnsureSuccessStatusCodeAsync();            
            return await response.Content.ReadFromJsonAsync<ShoppingListData[]>();
        }
        public async Task<ShoppingListData> CreateShoppingList(ShoppingListData newData)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/ShoppingListCollection");
            request.Content = new StringContent(JsonConvert.SerializeObject(newData), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListData>();
        }
        public async Task DeleteShoppingList(string shoppingListId)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/ShoppingListCollection/{shoppingListId}");
            var response = await httpClient.SendAsync(request);
            await response.EnsureSuccessStatusCodeAsync(); ;
        }

        public async Task<ShoppingListData> GetShoppingList(string listId, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ShoppingList/{listId}");
            var response = await httpClient.SendAsync(request);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListData>(null, cancellationToken);            
        }

        public async Task<ShoppingListItemData> AddItemToShoppingList(string listId, ShoppingListItemData itemToAdd, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/ShoppingList/{listId}");
            request.Content = new StringContent(JsonConvert.SerializeObject(itemToAdd), System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListItemData>(null, cancellationToken);
        }

        public async Task DeleteItemFromShoppingList(string listId, string itemId, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/ShoppingList/{listId}/{itemId}");
            var response = await httpClient.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
        }

        public async Task<ShoppingListItemData> RankDownListItem(string listId, string itemId, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/ShoppingList/{listId}/{itemId}/rankdown");
            var response = await httpClient.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListItemData>(null, cancellationToken);
        }

        public async Task<ShoppingListItemData> RankUpListItem(string listId, string itemId, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/ShoppingList/{listId}/{itemId}/rankup");
            var response = await httpClient.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListItemData>(null, cancellationToken);
        }

        public async Task<ShoppingListItemData> UpdateListItem(string listId, ShoppingListItemData itemToUpdate, CancellationToken cancellationToken = default)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/ShoppingList/{listId}");
            request.Content = JsonContent.Create<ShoppingListItemData>(itemToUpdate);
            var response = await httpClient.SendAsync(request, cancellationToken);
            await response.EnsureSuccessStatusCodeAsync();
            return await response.Content.ReadFromJsonAsync<ShoppingListItemData>(null, cancellationToken);
        }


    }
}
