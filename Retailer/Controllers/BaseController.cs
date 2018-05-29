using System;
using Microsoft.AspNetCore.Mvc;

namespace RetailerApi.Controllers
{
    public class BaseController : Controller
    {
        [Route("app/error")]
        public virtual IActionResult Error(int? statusCode = null, Exception ex = null)
        {
            #if DEBUG
            if (ex != null)
                return new JsonResult(new { ex = ex.Message });
            return BadRequest();
            #else
                return StatusCode("Status core : "(int)statusCode);
            #endif
        }
    }
}