using clothes_store.Repository;
using Microsoft.AspNetCore.Mvc;


namespace clothes_store.ViewComponents
{
    public class RecommendedViewComponent: ViewComponent
    {
        private readonly IProduct _pro;
        public RecommendedViewComponent(IProduct product)
        {
            _pro = product;
        }
        public IViewComponentResult Invoke()
        {
            var pro = _pro.top10pro();
            return View(pro);
        }
    }
}
