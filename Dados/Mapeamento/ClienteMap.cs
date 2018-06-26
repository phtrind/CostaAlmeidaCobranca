using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Map(x => x.TelefoneFixo).ToColumn("CLI_TELFIXO");
            Map(x => x.TelefoneCelular).ToColumn("CLI_TELCELULAR");
            Map(x => x.DataNascimento).ToColumn("CLI_DTHNASCIMENTO");

            Map(x => x.IdEndereco).ToColumn("END_CODIGO");
            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");

            Map(x => x.DataCadastro).ToColumn("CLI_DTHCADASTRO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Endereco).Ignore();
            Map(x => x.Contratos).Ignore();
            Map(x => x.ContasBancarias).Ignore();
        }
    }
}
