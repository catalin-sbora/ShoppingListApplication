using ShopListDemo.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Domain.Abstractions
{
    public interface IUserRepository
    {
        Task<ShoppingAppUser> GetById(string id);
        Task<ShoppingAppUser> GetByExternalId(string externalId);

        Task<ShoppingAppUser> Add(ShoppingAppUser appUser);
    }
}
