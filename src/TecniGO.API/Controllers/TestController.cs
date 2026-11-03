using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TecniGO.API.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok(new { mensaje = "¡TecniGO API está funcionando correctamente! 🚀", estado = "Activo" });
        }
    }
}