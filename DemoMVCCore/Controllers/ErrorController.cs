using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoMVCCore.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        // GET: /<controller>/
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, cannot find that page you requested";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    _logger.LogWarning($"404 Error:The path {statusCodeResult.OriginalPath}|QueryString:{statusCodeResult.OriginalQueryString}");
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry, cannot find that page you requested";
                    ViewBag.Path = statusCodeResult.OriginalPath;
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    _logger.LogWarning($"404 Error:The path {statusCodeResult.OriginalPath}|QueryString:{statusCodeResult.OriginalQueryString}");
                    return View("Error");
                    break;
            }
            return View("NotFound");
        }
        
        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var excepDets = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = excepDets.Path;
            ViewBag.ExceptionMessage = excepDets.Error.Message;
            ViewBag.Stacktrace = excepDets.Error.StackTrace;
            _logger.LogError($"The path {excepDets.Path}|{excepDets.Error.Message}|{excepDets.Error.StackTrace}");


            return View("Error");
        }
    }
}
