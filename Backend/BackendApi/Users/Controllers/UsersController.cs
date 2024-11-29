using Microsoft.AspNetCore.Mvc;

namespace UsersApi.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
