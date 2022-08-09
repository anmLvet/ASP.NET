using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Bookmarks.Models {

    public class ViewBagActionFilter : ActionFilterAttribute
    {
        private readonly IOptions<MenuOptions> _settings;
         private readonly ILogger<ViewBagActionFilter> _logger;

        public ViewBagActionFilter(IOptions<MenuOptions> settings, ILogger<ViewBagActionFilter> logger){
            this._settings = settings;

            this._logger = logger;
            //DI will inject what you need here
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Controller is PageModel)
            {
                var controller = context.Controller as PageModel;
                controller.ViewData.Add("MenuItems", _settings.Value.Items);
            }

            if (context.Controller is Controller)
            {
                var controller = context.Controller as Controller;
                controller.ViewData.Add("MenuItems", _settings.Value.Items);
            }

            base.OnResultExecuting(context);
        }
    }
}