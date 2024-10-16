using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebUI.ViewComponents.Default
{
    public class _HeadPartial :ViewComponent
    {
        public IViewComponentResult Invoke() // çağırmak invoke
        {
            return View();
        }
    }
}
