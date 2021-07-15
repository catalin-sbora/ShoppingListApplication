using System;
using Microsoft.AspNetCore.Mvc;
using ShopListDemo.Application.Exceptions;



namespace ShopListDemo.Server.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult TranslateException(this ControllerBase controller, Exception exceptionToTranslate)
        {
            IActionResult result = controller.StatusCode(500, "Request cannot be processed due to some internal errors"); 
            switch (exceptionToTranslate) 
            {
                case ArgumentException e:
                    result = controller.BadRequest(e.Message);
                    break;
                case ItemNotInTheListException e:
                    result = controller.NotFound("The specified item is not in the current list");
                    break;
                case ListDoesNotExistException e:
                    result = controller.NotFound("The list you specified does not exist for the current user");
                    break;
                case UserDoesNotExistException e:
                    result = controller.NotFound("The information regarding the current user could not be found.");
                    break;
            }

            return result;
        }
    }
}
 