using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using IssueTracker.Api.Authentication;
using IssueTracker.Data;
using IssueTracker.Data.Infrastructure;
using IssueTracker.Services.Authentication;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NJsonSchema.Generation;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace IssueTracker.Api
{
    public class AssignPropertyRequiredFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var requiredProperties = context.Type.GetProperties()
                .Where(x => x.IsDefined(typeof(NotNullAttribute)))
                .Select(t => char.ToLowerInvariant(t.Name[0]) + t.Name.Substring(1));

            if (schema.Required == null)
            {
                schema.Required = new HashSet<string>();
            }
            schema.Required = schema.Required.Union(requiredProperties).ToHashSet();
        }
    }
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
            services.AddControllers();
            services.AddCors();
            
            services.AddAuthentication(
                    CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();
            
            services
                .AddEntityFrameworkNpgsql()
                .AddDbContextFactory<ApplicationDbContext>(a => a.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddAuthentication(ApiAuthenticationSchemeOptions.DefaultSchemeName)
                .AddScheme<ApiAuthenticationSchemeOptions, ApiAuthenticationHandler>(
                    ApiAuthenticationSchemeOptions.DefaultSchemeName, 
                    null);
            
            services.AddOpenApiDocument(document =>
            {
                document.Description = "IssueTracker Api";
                document.DefaultReferenceTypeNullHandling = ReferenceTypeNullHandling.Null;
                document.DefaultResponseReferenceTypeNullHandling = ReferenceTypeNullHandling.NotNull;
            });

            services
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddScoped<IContextFactory, ContextFactory>();
            
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(ILoginService), typeof(IContextFactory))
                .AddClasses(member => member.Where(type => type.GetInterfaces().Any(i => i.Name.EndsWith(type.Name))))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(
                    options => options.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                );
                app.UseDeveloperExceptionPage();
                app.UseOpenApi(p => p.Path = "/swagger/{documentName}/swagger.yaml");
                app.UseSwaggerUi3(p => p.DocumentPath = "/swagger/{documentName}/swagger.yaml");
                
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
