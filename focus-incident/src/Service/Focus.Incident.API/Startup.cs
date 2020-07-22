using Focus.Incident.API.StartUp;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Focus.Incident.API
{
    public class Startup
    {
        public IConfiguration configuration { get; }
        private IHostingEnvironment env { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddCustomCors();
            services.AddOData();
            //services.AddMemoryCache();

            services.AddMvc(options =>
            {
                if (!env.IsDevelopment())
                {
                    // require SSL
                    if (configuration["Kestrel:EndPoints:Https:Url"] != null)
                    {
                        var url = configuration["Kestrel:EndPoints:Https:Url"];
                        var port = url.Substring(url.LastIndexOf(':') + 1);
                        options.SslPort = int.Parse(port);
                        options.Filters.Add(new RequireHttpsAttribute());
                    }
                }

                // The default policy is to make sure that both authentication schemes - Cookie and Jwt - are challenged
                var defaultPolicy = new AuthorizationPolicyBuilder(CookieAuthenticationDefaults.AuthenticationScheme, IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .RequireAssertion(c => true) // A requirement is mandatory
                .Build();
                options.Filters.Add(new AuthorizeFilter(defaultPolicy));
            })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver(); // avoid camel case
                    options.SerializerSettings.Formatting = Formatting.Indented; // indent json output
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; // ignore self-referencing loops 
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()); // string names for enum types
                });

            services.AddAntiforgery(options =>
            {
                options.Cookie.Name = "_af";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = !env.IsDevelopment() ? CookieSecurePolicy.Always : CookieSecurePolicy.None;
                options.HeaderName = "X-XSRF-TOKEN";
            });

            services.AddRouting();
            services.AddCustomConfig(configuration);
            services.AddCustomServices(configuration);

            this.ConfigureAuth(services);
            services.AddCustomAuthorization();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseRewriter(new RewriteOptions().AddRedirectToHttps());

            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseStatusCodePages(); // this middleware adds simple, text-only handlers for common status codes, such as 404
            app.AddCustomOData();
        }

        protected virtual void ConfigureAuth(IServiceCollection services)
        {
            services.AddIdentityAuthentication(configuration, env);
        }
    }
}
