using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Basic
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v0.1", new Info { Title = "Basic Sample", Version = "v0.1" });
				c.CustomSchemaIds(x => x.FullName);
				c.DescribeAllEnumsAsStrings();
				c.DescribeStringEnumsInCamelCase();
				c.DescribeAllParametersInCamelCase();

				c.TagActionsBy(api =>
				{
					return ((ControllerActionDescriptor)api.ActionDescriptor).ControllerTypeInfo.Namespace;
				});
			});

			services
				.AddMvc()
				.AddNestedRouting(useKebabCase: true);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v0.1/swagger.json", "Basic Sample");
			});

			app.UseMvc();
		}
	}
}
