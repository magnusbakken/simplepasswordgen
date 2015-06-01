namespace PasswordGen.WebApi.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using PasswordGen.RandomProviders;
    using PasswordGen.WebApi.Models;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Generate a password";
            var model = new GeneratePasswordBindingModel()
            {
                Providers = Enum.GetValues(typeof(RandomProviderType))
                    .Cast<RandomProviderType>()
                    .Select(rpt => new SelectListItem() { Value = ((int)rpt).ToString(), Text = GeneratePasswordBindingModel.GetProviderDescription(rpt) })
                    .ToList()
            };

            return View(model);
        }
    }
}