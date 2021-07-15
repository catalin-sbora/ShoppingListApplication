using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Domain.Abstractions
{
    public interface IPersistenceContext
    {
        Task InitializeAsync();
        IUserRepository GetUserRepository();
        Task SaveChangesAsync();
    }
}
