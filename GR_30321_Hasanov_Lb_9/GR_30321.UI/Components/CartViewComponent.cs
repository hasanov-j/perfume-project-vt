using Microsoft.AspNetCore.Mvc;

namespace GR_30321.UI.Components
{
    public class CartViewComponent: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
