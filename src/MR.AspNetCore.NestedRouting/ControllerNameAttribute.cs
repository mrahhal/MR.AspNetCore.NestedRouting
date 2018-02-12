using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace MR.AspNetCore.NestedRouting
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class ControllerNameAttribute : Attribute, IControllerModelConvention
	{
		private readonly string _controllerName;

		public ControllerNameAttribute(string controllerName)
		{
			_controllerName = controllerName;
		}

		public void Apply(ControllerModel model)
		{
			model.ControllerName = _controllerName;
		}
	}
}
