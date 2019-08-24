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
                app.UseCors(CorsOptions.AllowAll);//this is very important line cross orgin source(CORS)it is used to enable cross-site HTTP requests  
                                                  //For security reasons, browsers restrict cross-origin HTTP requests 
            var OAuthOptions = new OAuthAuthorizationServerOptions
                {
                    AllowInsecureHttp = true,
                    TokenEndpointPath = new PathString("/token"),
                    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),//token expiration time
                    Provider = new OauthProvider()
                };

                app.UseOAuthBearerTokens(OAuthOptions);
                app.UseOAuthAuthorizationServer(OAuthOptions);
                app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

                HttpConfiguration config = new HttpConfiguration();
                WebApiConfig.Register(config);//register the request
            }

            public void Configuration(IAppBuilder app)
            {
                ConfigureAuth(app);
                GlobalConfiguration.Configure(WebApiConfig.Register);
            }

        }
   
}