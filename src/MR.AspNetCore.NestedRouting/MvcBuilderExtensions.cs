using System;
using MR.AspNetCore.NestedRouting;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class MvcBuilderExtensions
	{
		public static IMvcBuilder AddNestedRouting(
			this IMvcBuilder builder,
			bool useKebabCase = false)
		{
			return builder.AddNestedRouting(options =>
			{
				options.UseKebabCase = true;
			});
		}

		public static IMvcBuilder AddNestedRouting(
			this IMvcBuilder builder,
			Action<NestedRoutingOptions> optionsAction = null)
		{
			var nestedRoutingOptions = new NestedRoutingOptions();
			optionsAction?.Invoke(nestedRoutingOptions);

			builder.AddMvcOptions(options =>
			{
				options.Conventions.Add(new RoutingConvention(nestedRoutingOptions));
			});

			return builder;
		}
	}
}
