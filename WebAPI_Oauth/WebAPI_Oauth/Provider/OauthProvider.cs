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
            //First request will come here, this method will validate the request wheather it has crendtials(UserName and Password) if the request not contain username and 
           //password the request will reject from here not proceded any further
            context.Validated(); 
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //If the request has valid and it contain username and password than this method will check correct crenstials and than generate a valid token
            var identity = new ClaimsIdentity(context.Options.AuthenticationType); //it will check the authenticate type

            using (var db = new DataContext())
            {
                if (db != null)
                {
                    var user = db.Users.Where(o => o.UserName == context.UserName && o.Password == context.Password).FirstOrDefault();//LINQ query checking the username 
                    //and password from db
                    if (user != null)
                    {
                        //Store information againest the request
                        identity.AddClaim(new Claim("UserName", context.UserName));
                        identity.AddClaim(new Claim("LoggedOn", DateTime.Now.ToString()));
                        context.Validated(identity);
                    }
                    else
                    {
                        context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                        context.Rejected();

                    }
                }
                else
                {
                    context.SetError("Wrong Crendtials", "Provided username and password is incorrect");
                    context.Rejected();
                }
                return;
            }
        }
    }
}