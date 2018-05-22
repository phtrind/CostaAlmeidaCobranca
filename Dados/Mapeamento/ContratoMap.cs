using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class ContratoMap : DommelEntityMap<ContratoEntidade>
    {
        public ContratoMap()
        {
            ToTable("CONTRATOS");

            Map(x => x.Id).ToColumn("COD_CODCONTRATO").IsKey().IsIdentity();
            Map(x => x.Valor).ToColumn("VAL_VALORCONTRATO");
            Map(x => x.TaxaLucro).ToColumn("VAL_TAXALUCRO");
            Map(x => x.Observacao).ToColumn("TXT_OBSERVACAO");
            Map(x => x.Status).ToColumn("IDC_STATUSCONTRATO");

            Map(x => x.IdEvento).ToColumn("COD_CODEVENTO");
            Map(x => x.IdUsuario).ToColumn("COD_CODUSUARIO");
            Map(x => x.IdVendedor).ToColumn("COD_CODVENDEDOR");
            Map(x => x.IdComprador).ToColumn("COD_CODCOMPRADOR");

            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROCONTRATO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Evento).Ignore();
            Map(x => x.Vendedor).Ignore();
            Map(x => x.Comprador).Ignore();
            Map(x => x.Parcelas).Ignore();
        }
    }
}
