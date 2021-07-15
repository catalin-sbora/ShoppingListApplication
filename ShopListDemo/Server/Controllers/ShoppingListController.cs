using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopListDemo.Application;
using ShopListDemo.Domain.Abstractions;
using ShopListDemo.Server.Extensions;
using ShopListDemo.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ShopListDemo.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IPersistenceContext persistenceContext;

        private ShoppingListService GetShopListService()
        {
            string userId = User.Claims
                                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                                .Value;
            return new ShoppingListService(persistenceContext, userId);
        }
        public ShoppingListController(IPersistenceContext persistenceContext)
        {
            this.persistenceContext = persistenceContext;
        }

        [HttpGet("{listId}")]
        public async Task<IActionResult> GetShoppingList([FromRoute] string listId)
        {
            IActionResult result = null;
            try
            {
                var list = await GetShopListService()
                            .GetShoppingList(listId);
                result = Ok(list);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
        }

        [HttpPost("{listId}")]
        public async Task<IActionResult> AddListItem([FromRoute]string listId, [FromBody] ShoppingListItemData item)
        {
            IActionResult result = null;

            try
            {
                var newItem = await GetShopListService()
                                    .AddListItem(listId, item);
                result = Ok(newItem);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result; 
        }

        [HttpPut("{listId}")]
        public async Task<IActionResult> UpdateListItem([FromRoute] string listId, [FromBody] ShoppingListItemData item)
        {
            IActionResult result = null;
            try
            {
                var newItem = await GetShopListService()
                            .UpdateListItem(listId, item);
                result = Ok(newItem);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
           
        }

        [HttpDelete("{listId}/{itemId}")]
        public async Task<IActionResult> DeleteListItem([FromRoute] string listId, [FromRoute] string itemId)
        {
            IActionResult result = null;
            try
            {
                await GetShopListService()
                    .RemoveListItem(listId, itemId);
                result = Ok();
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
        }

        [HttpPut("{listId}/{itemId}/rankdown")]
        public async Task<IActionResult> RankDownListItem([FromRoute] string listId, [FromRoute] string itemId)
        {
            IActionResult result = null;
            try
            {
                var updatedItem = await GetShopListService()
                        .RankDownItem(listId, itemId);
                result = Ok(updatedItem);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;           
        }

        [HttpPut("{listId}/{itemId}/rankup")]
        public async Task<IActionResult> RankUpListItem([FromRoute] string listId, [FromRoute] string itemId)
        {
            IActionResult result = null;
            try
            {
                var updatedItem = await GetShopListService()
                       .RankUpItem(listId, itemId);
                result = Ok(updatedItem);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;            
        }

    }
}
