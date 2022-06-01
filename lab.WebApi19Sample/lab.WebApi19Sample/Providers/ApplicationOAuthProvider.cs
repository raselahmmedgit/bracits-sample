using Microsoft.AspNet.Identity.Owin;
using Microsoft.Extensions.Logging;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace lab.WebApi19Sample.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly ILogger<ApplicationOAuthProvider> _logger;

        public ApplicationOAuthProvider(string publicClientId)
        {
            _logger.LogInformation("Method: ApplicationOAuthProvider ");
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            _logger.LogInformation("Method: GrantResourceOwnerCredentials ");
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();

            #region Login: Code will be change.

            //var userNameEncpt = context.UserName.ToLower().ToEncryptString();
            //var userList = await userManager.Users.Where(x => x.Email == userNameEncpt).ToListAsync();
            //if (userList.Any() == false)
            //{
            //    context.SetError("invalid_grant", "Sorry, OnTrack Health doesn't recognize that email.");
            //    return;
            //}
            //var isActiveUserList = userList.Any(x => x.IsDeleted == false);
            //if (isActiveUserList == false)
            //{
            //    context.SetError("invalid_grant", "Your account is not active anymore.");
            //    return;
            //}

            //var userName = userList.FirstOrDefault().Id;

            #endregion

            //ApplicationUser user = await userManager.FindAsync(userName, context.Password);
            ApplicationUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                _logger.LogInformation("Method: GrantResourceOwnerCredentials The user name or password is incorrect.");

                return;
            }
            _logger.LogInformation("Method: GrantResourceOwnerCredentials " + user.Email);
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);
            _logger.LogInformation("Method: GrantResourceOwnerCredentials oAuthIdentity " + oAuthIdentity + " " + cookiesIdentity);
            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            _logger.LogInformation("Method: GrantResourceOwnerCredentials " + ticket);
            context.Validated(ticket);
            _logger.LogInformation("Method: GrantResourceOwnerCredentials validated");
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            _logger.LogInformation("Method: TokenEndpoint ");
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            _logger.LogInformation("Method: ValidateClientAuthentication ");
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            _logger.LogInformation("Method: ValidateClientRedirectUri ");
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            _logger.LogInformation("Method: AuthenticationProperties ");
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

    }
}
