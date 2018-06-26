using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Dados;
using System.Web.Http.Cors;

[assembly: OwinStartup(typeof(CostaAlmeidaCobranca.Startup))]

namespace CostaAlmeidaCobranca
{
    //[EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            RegisterMappings.Register();

            config.MapHttpAttributeRoutes();

            config.EnableCors(new EnableCorsAttribute(origins: "*", headers: "*", methods: "*"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ConfigureAccessToken(app);

            app.UseWebApi(config);

            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.JsonFormatter.Indent = true;
            //config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "yyyy-MM-dd";
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
                TokenEndpointPath = new PathString("/token"),

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
