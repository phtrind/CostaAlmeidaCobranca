using Dados;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(CostaAlmeidaCobranca.Startup))]

namespace CostaAlmeidaCobranca
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            RegisterMappings.Register();

            config.MapHttpAttributeRoutes();

            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureAccessToken(app);

            app.UseWebApi(config);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;
        }

        private void ConfigureAccessToken(IAppBuilder app)
        {
            var optionsConfigurationToken = new OAuthAuthorizationServerOptions()
            {
                //Permitindo acesso ao endereço de fornecimento do token de acesso sem 
                //precisar de HTTPS (AllowInsecureHttp). 
                //Em produção o valor deve ser false.
                AllowInsecureHttp = true,

                //Configurando o endereço do fornecimento do token de acesso (TokenEndpointPath).
                TokenEndpointPath = new PathString("/api/token"),

                //Configurando por quanto tempo um token de acesso já forncedido valerá (AccessTokenExpireTimeSpan).
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(20),

                //Como verificar usuário e senha para fornecer tokens de acesso? Precisamos configurar o Provider dos tokens
                Provider = new ProviderTokenAccess()
            };

            //Estas duas linhas ativam o fornecimento de tokens de acesso numa WebApi
            app.UseOAuthAuthorizationServer(optionsConfigurationToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
