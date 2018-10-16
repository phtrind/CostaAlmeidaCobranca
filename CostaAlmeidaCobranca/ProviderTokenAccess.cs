using Microsoft.Owin.Security.OAuth;
using Negocio;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CostaAlmeidaCobranca
{
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
                context.SetError("-1", "Credenciais não encontradas.");
            }
        }
    }
}
