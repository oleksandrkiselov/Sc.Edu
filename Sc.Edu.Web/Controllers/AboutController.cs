using System.Web.Mvc;
using Sc.Edu.Web.Models;
using Sitecore.Mvc.Presentation;

namespace Sc.Edu.Web.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult Index()
        {
            var dataSourceItem = RenderingContext.Current?.Rendering.Item;
            var model = new AboutViewModel()
            {
                Item = dataSourceItem
            };

            return View(model);
        }
    }
}