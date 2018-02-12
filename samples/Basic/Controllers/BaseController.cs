using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Basic.Controllers
{
	[Route("api/v{version}/[controller]")]
	public abstract class BaseController : Controller
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
			Version = (string)context.RouteData.Values["version"];
		}

		public override void OnActionExecuted(ActionExecutedContext context)
		{
			base.OnActionExecuted(context);
			if (!context.HttpContext.Response.HasStarted)
			{
				context.HttpContext.Response.Headers.Add("Api-Version", Version);
			}
		}

		public string Version { get; set; }
	}
}
