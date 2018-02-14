using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MR.AspNetCore.NestedRouting.Internal;

namespace MR.AspNetCore.NestedRouting
{
	public class NestedRoutingConvention : IApplicationModelConvention
	{
		private const string BaseToken = "[base]";
		private const string ControllerToken = "[controller]";
		private readonly NestedRoutingOptions _options;

		public NestedRoutingConvention(
			NestedRoutingOptions options)
		{
			_options = options;
		}

		public void Apply(ApplicationModel application)
		{
			foreach (var controller in application.Controllers)
			{
				ProcessControllerName(controller);

				var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
				foreach (var selectorModel in matchedSelectors)
				{
					Process(selectorModel.AttributeRouteModel, controller.ControllerType);
				}
			}
		}

		private void ProcessControllerName(ControllerModel controller)
		{
			if (controller.Attributes.OfType<ControllerNameAttribute>().Any())
			{
				return;
			}

			if (_options.UseKebabCase)
			{
				var name = controller.ControllerName;
				controller.ControllerName = name.ToKebabCase();
			}
		}

		private void Process(AttributeRouteModel model, Type type)
		{
			var routeAttr = type.GetCustomAttributes(false).OfType<RouteAttribute>().FirstOrDefault();
			var template = routeAttr?.Template ?? string.Empty;

			var @base = BuildBase(type.BaseType, !string.IsNullOrEmpty(template));
			template = @base + StripBaseToken(template);

			model.Template = template;
		}

		private string BuildBase(Type type, bool stripController)
		{
			if (type == null)
			{
				return string.Empty;
			}

			var template = string.Empty;
			var @base = string.Empty;
			var routeAttr = type.GetCustomAttributes(false).OfType<RouteAttribute>().FirstOrDefault();

			if (routeAttr != null)
			{
				template = routeAttr.Template;
				if (stripController)
				{
					template = StripControllerToken(template);
				}

				if (template.Contains(BaseToken))
				{
					@base = BuildBase(type.BaseType, true);
					return @base + StripBaseToken(template);
				}
				else
				{
					return template;
				}
			}

			@base = BuildBase(type.BaseType, true);
			return @base + template;
		}

		private string StripBaseToken(string template)
		{
			if (template.Contains(BaseToken))
			{
				return template.Substring(BaseToken.Length);
			}
			return template;
		}

		private string StripControllerToken(string template)
		{
			if (template.Contains(ControllerToken))
			{
				return template.Substring(0, template.Length - ControllerToken.Length - 1);
			}
			return template;
		}
	}
}
