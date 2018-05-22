using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class ParcelaMap : DommelEntityMap<ParcelasEntidade>
    {
        public ParcelaMap()
        {
            ToTable("PARCELAS");

            Map(x => x.Id).ToColumn("COD_CODPARCELA").IsKey().IsIdentity();

            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROPARCELA");

            Map(x => x.Valor).ToColumn("VAL_VALORPARCELA");
            Map(x => x.Vencimento).ToColumn("DTH_VENCIMENTO");
            Map(x => x.Status).ToColumn("IDC_STATUSPARCELA");
            Map(x => x.Juros).ToColumn("VAL_JUROSPARCELA");

            Map(x => x.IdContrato).ToColumn("COD_CODCONTRATO");

            Map(x => x.Contrato).Ignore();
        }
    }
}
