using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class ClienteMap : DommelEntityMap<ClienteEntidade>
    {
        public ClienteMap()
        {
            ToTable("CLI_CLIENTES");

            Map(x => x.Id).ToColumn("CLI_CODIGO").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("CLI_NOME");
            Map(x => x.Cpf).ToColumn("CLI_CPF");
            Map(x => x.Email).ToColumn("CLI_EMAIL");
            Map(x => x.Fazenda).ToColumn("CLI_FAZENDA");
            Map(x => x.TelefoneFixo).ToColumn("CLI_TELFIXO");
            Map(x => x.TelefoneCelular).ToColumn("CLI_TELCELULAR");

            Map(x => x.IdEndereco).ToColumn("END_CODIGO");
            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_ALTERACAO");

            Map(x => x.DataCadastro).ToColumn("CLI_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("CLI_DTHALTERACAO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.UsuarioAlteracao).Ignore();
            Map(x => x.Endereco).Ignore();
            Map(x => x.Contratos).Ignore();
            Map(x => x.ContasBancarias).Ignore();
        }
    }
}
