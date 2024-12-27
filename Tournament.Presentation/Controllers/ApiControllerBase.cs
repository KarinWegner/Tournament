using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tournament.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tournament.Presentation.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        public ActionResult ProcessError(ApiBaseResponse baseResponse)
        {
            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound( 
                (
                    Detail: ((ApiNotFoundResponse)baseResponse).Message,
                     Status: StatusCodes.Status404NotFound,
                    Title: "Not Found",
                    instance: HttpContext.Request.Path
                )),
                BadRequestResponse => BadRequest(
                    (
                    Detail: ((BadRequestResponse)baseResponse).Message,
                     Status: StatusCodes.Status404NotFound,
                    Title: "Bad Request",
                    instance: HttpContext.Request.Path

                )),
                _ => throw new NotImplementedException()
            };

        }
    }
}