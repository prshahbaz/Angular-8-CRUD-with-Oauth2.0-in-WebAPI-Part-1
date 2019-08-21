using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using WebAPI_Oauth.Models;

namespace WebAPI_Oauth.Provider
{
    public class OauthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); //   
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            using (var db = new DataContext())
            {
                if (db != null)
                {
                    var user = db.Users.Where(o => o.UserName == context.UserName && o.Password == context.Password).FirstOrDefault();
                    if (user != null)
                    {
                        identity.AddClaim(new Claim("UserName", context.UserName));
                        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("invalid_grant", "Provided username and password is incorrect");
                        context.Rejected();

                    }
                }
                else
                {
                    context.SetError("invalid_grant", "Provided username and password is incorrect");
                    context.Rejected();
                }
                return;
            }
        }
    }
}