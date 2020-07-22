//using Focus.Infrastructure.Domain.Focus;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;

namespace Focus.Incident.API.StartUp
{
    public static partial class AuthorizationExtensions
    {
        public static void AddCustomAuthorization(this IServiceCollection services)
        {
            //var serviceProvider = services.BuildServiceProvider();
            //var authorizationService = serviceProvider.GetService<Domain.Common.Interface.IAuthorizationService>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsLoggedIn", policy =>
                {
                    policy.RequireAssertion(context =>
                  {
                      var identity = ((ClaimsIdentity)context.User.Identity);
                      if (!identity.IsAuthenticated) return false;
                      return true;
                  });
                });
                //options.AddPolicy("CanUpdatePatchExclusionStatuses", policy =>
                //{
                //    policy.RequireAssertion(async context =>
                //    {
                //        var identity = ((ClaimsIdentity)context.User.Identity);
                //        if (!identity.IsAuthenticated) return false;

                //        var userId = identity.Claims.FirstOrDefault(x => x.Type == "sub");
                //        if (userId == null) throw new Exception("Invalid 'sub' claim.");

                //        return await authorizationService.IsUserInRole(userId.Value, Constants.Role_Administrator, null, null, null) ||
                //            await authorizationService.IsUserInRole(userId.Value, Constants.Role_PatchManager, null, null, null);
                //    });
                //}); 
            });
        }
    }
}
