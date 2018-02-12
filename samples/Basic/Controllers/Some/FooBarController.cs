using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Basic.Controllers.Some
{
	public class FooBarController : SomeBaseController
	{
		// GET api/values
		[HttpGet]
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}
	}
}
