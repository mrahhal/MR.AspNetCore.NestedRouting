using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers
{
	public class RootController : Controller
	{
		[HttpGet("")]
		public IActionResult Get()
		{
			return Redirect("/swagger");
		}
	}
}
