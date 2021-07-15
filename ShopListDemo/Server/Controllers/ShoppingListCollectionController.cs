using Microsoft.AspNetCore.Authorization;
using ShopListDemo.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using ShopListDemo.Application;
using ShopListDemo.Domain.Abstractions;
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
    public class ShoppingListCollectionController : ControllerBase
    {
        private readonly IPersistenceContext dataContext;
        private ShoppingListCollectionService GetShopListCollectionService()
        {
            string userId = User.Claims
                                .FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))
                                .Value;
            return new ShoppingListCollectionService(dataContext, userId);
        }

        public ShoppingListCollectionController(IPersistenceContext dataContext)
        {
         
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetShoppingListCollection()
        {
            
            IActionResult result = null;
            try
            {
                var list = await GetShopListCollectionService()
                                .GetShoppingListCollection();
                result = Ok(list);
            }
            catch (Exception e)
            {
               result = this.TranslateException(e);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> AddShoppingList([FromBody] ShoppingListData shoppingList)
        {
            IActionResult result = null;
            try
            {
                var newList = await GetShopListCollectionService()
                      .CreateShoppingList(shoppingList.Name);
                result = Ok(newList);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
        }

        [HttpDelete("{listId}")]
        public async Task<IActionResult> RemoveShoppingList([FromRoute] string listId)
        {
            IActionResult result = null;
            try
            {
                await GetShopListCollectionService()
                        .RemoveShoppingList(listId);
                result = Ok();
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShoppingList([FromBody] ShoppingListData shoppingList)
        {
            IActionResult result = null;

            try
            {
                var list = await GetShopListCollectionService()
                                .UpdateShoppingList(shoppingList);

                result = Ok(list);
            }
            catch (Exception e)
            {
                result = this.TranslateException(e);
            }
            return result;
        }
    }
}
