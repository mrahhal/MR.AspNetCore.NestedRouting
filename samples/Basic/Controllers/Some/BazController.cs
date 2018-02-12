using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MR.AspNetCore.NestedRouting;

namespace Basic.Controllers.Some
{
	[ControllerName("bazzzzzz")]
	public class BazController : SomeBaseController
	{
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}
