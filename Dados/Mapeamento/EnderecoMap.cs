using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class EnderecoMap : DommelEntityMap<EnderecoEntidade>
    {
        public EnderecoMap()
        {
            ToTable("ENDERECOS");

            Map(x => x.Id).ToColumn("COD_CODENDERECO").IsKey().IsIdentity();
            Map(x => x.Logradouro).ToColumn("TXT_LOGRADOURO");
            Map(x => x.Numero).ToColumn("TXT_NUMERO");
            Map(x => x.Complemento).ToColumn("TXT_COMPLEMENTO");
            Map(x => x.Cep).ToColumn("NUM_CEP");
            Map(x => x.Cidade).ToColumn("TXT_CIDADE");
            Map(x => x.Estado).ToColumn("TXT_ESTADO");
            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROENDERECO");
        }
    }
}
