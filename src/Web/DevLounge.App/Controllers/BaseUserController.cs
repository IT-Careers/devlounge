using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevLounge.Web.Controllers
{
    [Authorize(Roles = "User,Admin")]
    public abstract class BaseUserController : Controller
    {
    }
}
