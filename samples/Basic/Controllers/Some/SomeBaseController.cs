using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers.Some
{
	[Route("[base]/some/[controller]")]
	public abstract class SomeBaseController : BaseController
	{
	}
}
