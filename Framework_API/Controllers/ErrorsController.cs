using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Framework_API.Controllers
{
    public class ErrorsController : Controller
    {
        [HttpGet("Errors/{errorCode}")]
        public IActionResult Index(int errorCode)
        {
            return View(errorCode);
        }
    }
}
