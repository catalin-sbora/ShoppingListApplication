using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopListDemo.Application.Exceptions
{
    public class ListDoesNotExistException: Exception
    {
        public string UserId { get; private set; }
        public string ListId { get; private set; }
        public ListDoesNotExistException(string userId, string listId) : base($"The list {listId} doesn't exist for user {userId}")
        {
            UserId = userId;
            ListId = listId;
        }
    }
}
