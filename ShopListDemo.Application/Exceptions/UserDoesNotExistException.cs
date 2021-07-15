using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Application.Exceptions
{
    public class UserDoesNotExistException:Exception
    {
        public string ExternalUserId { get; private set; }
        public UserDoesNotExistException(string externalUserId) : base($"The user with external id: '{externalUserId}' does not exist")
        {
            ExternalUserId = externalUserId;
        }
    }
}
