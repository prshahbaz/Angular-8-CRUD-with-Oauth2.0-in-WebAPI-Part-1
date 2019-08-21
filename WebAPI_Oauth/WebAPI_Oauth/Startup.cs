using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAPI_Oauth.Provider;

namespace WebAPI_Oauth
{
 
        public class Startup
        {
            public void ConfigureAuth(IAppBuilder app)
            {
                app.UseCors(CorsOptions.AllowAll);
                var OAuthOptions = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                    Provider = new OauthProvider()
                };

                app.UseOAuthBearerTokens(OAuthOptions);
                app.UseOAuthAuthorizationServer(OAuthOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

                HttpConfiguration config = new HttpConfiguration();
                WebApiConfig.Register(config);
            }

            public void Configuration(IAppBuilder app)
            {
                ConfigureAuth(app);
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }

        }
   
}