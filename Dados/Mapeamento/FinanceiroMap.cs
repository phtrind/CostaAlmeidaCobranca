using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class FinanceiroMap : DommelEntityMap<FinanceiroEntidade>
    {
        public FinanceiroMap()
        {
            ToTable("TRANSACOES");

            Map(x => x.Id).ToColumn("COD_CODTRANSACAO").IsKey().IsIdentity();
            Map(x => x.Valor).ToColumn("VAL_VALOR");
            Map(x => x.Tipo).ToColumn("IDC_TIPO");
            Map(x => x.Data).ToColumn("DTH_DATA");
            Map(x => x.Status).ToColumn("IDC_STATUS");
            Map(x => x.IdUsuario).ToColumn("COD_CODUSUARIO");
            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROTRANSACAO");

            Map(x => x.Usuario).Ignore();
        }
    }
}
