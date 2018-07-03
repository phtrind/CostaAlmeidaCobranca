using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Web;
using System.Security.Claims;
using Negocio;
using System.Web.Http.Cors;

namespace CostaAlmeidaCobranca
{
    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    internal class ProviderTokenAccess : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication
            (OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var negocioUsuario = new UsuarioNegocio();

            if (negocioUsuario.VerificarLogin(context.UserName, context.Password))
            {
                var identyUser = new ClaimsIdentity(context.Options.AuthenticationType);
                identyUser.AddClaim(new Claim(ClaimTypes.Role, "user"));

                context.Validated(identyUser);
            }
            else
            {
                context.SetError("invalid_grant",
                    "Credenciais não encontradas.");
            }
        }
    }
}
