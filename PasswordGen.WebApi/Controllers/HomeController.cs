using System.Web.Mvc;
using PasswordGen.WebApi.Models;

namespace PasswordGen.WebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Generate a password";
            return View(new GeneratePasswordBindingModel());
        }
    }
}