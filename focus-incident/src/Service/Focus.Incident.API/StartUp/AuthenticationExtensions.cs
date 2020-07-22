using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Focus.Incident.API.StartUp
{
    public static partial class Extensions
    {
        public static void AddIdentityAuthentication(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment env)
        {
            //* Cookie & JWT authentication
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "VirtualScheme";
            })
            // mix authentication schemes depending of whether the request has a bearer token or if a cookie is used
            // this is to prevent redirecting to OpenIdConnect when bearer tokens are used (direct API calls via SPA website)
            .AddPolicyScheme(
                "VirtualScheme",
                "Virtual Scheme",
                options =>
                {
                    options.ForwardDefaultSelector = context =>
                    {
                        if (context.Request.Headers.Keys.Contains("Authorization"))
                        {
                            // JWT bearer
                            return JwtBearerDefaults.AuthenticationScheme;
                        }
                        return OpenIdConnectDefaults.AuthenticationScheme;
                    };
                })
              // Cookie middleware
              .AddCookie(options =>
              {
                  options.Cookie.Name = "Focus.Incident.API.AuthToken";
                  options.ForwardChallenge = OpenIdConnectDefaults.AuthenticationScheme;
              })

             // JWT bearer middleware
             .AddIdentityServerAuthentication(options =>
             { 
                 options.Authority = configuration["AUTHENTICATION_AUTHORITY"]; 
            
                 options.ApiName = configuration["Authentication:ApiName"];
                 options.ApiSecret = configuration["Authentication:ApiSecret"];
                 options.RequireHttpsMetadata = env.IsDevelopment() ? false : true; 
             })

              // OpenId Connect (implicit flow)
              .AddOpenIdConnect(options =>
              {
                  options.Authority = configuration["AUTHENTICATION_AUTHORITY"]; 

                  options.ClientId = configuration["Authentication:ClientId"];
                  options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                  options.RequireHttpsMetadata = env.IsDevelopment() ? false : true;

                  options.Scope.Clear();
                  options.Scope.Add("openid");
                  options.Scope.Add("profile");
              });

            // don't map standard JWT claim names to the classic MS claim names
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }
    }
}
