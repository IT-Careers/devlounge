using DevLounge.Web.Models.Error;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var feature = this.HttpContext.Features.Get<IExceptionHandlerFeature>();
            var error = feature.Error;

            return View(new ErrorModel
            {
                Message = error.Message,
                TimeStamp = DateTime.Now,
                Details = error
            });
        }
    }
}
