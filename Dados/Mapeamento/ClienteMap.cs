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
            ToTable("CLIENTES");

            Map(x => x.Id).ToColumn("COD_CODCLIENTE").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("TXT_NOME");
            Map(x => x.Cpf).ToColumn("TXT_CPF");
            Map(x => x.Email).ToColumn("TXT_EMAIL");
            Map(x => x.TelefoneFixo).ToColumn("TEL_TELEFONEFIXO");
            Map(x => x.TelefoneCelular).ToColumn("TEL_TELEFONECELULAR");
            Map(x => x.DataNascimento).ToColumn("DTH_DATANASCIMENTO");

            Map(x => x.IdEndereco).ToColumn("COD_CODENDERECO");
            Map(x => x.IdUsuario).ToColumn("COD_CODUSUARIO");

            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROCLIENTE");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Endereco).Ignore();
            Map(x => x.Contratos).Ignore();
            Map(x => x.ContasBancarias).Ignore();
        }
    }
}
