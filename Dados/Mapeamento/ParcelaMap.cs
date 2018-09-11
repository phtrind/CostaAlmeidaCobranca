using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class ParcelaMap : DommelEntityMap<ParcelasEntidade>
    {
        public ParcelaMap()
        {
            ToTable("PAR_PARCELAS");

            Map(x => x.Id).ToColumn("PAR_CODIGO").IsKey().IsIdentity();

            Map(x => x.Valor).ToColumn("PAR_VALOR");
            Map(x => x.TaxaLucro).ToColumn("PAR_TAXALUCRO");
            Map(x => x.Vencimento).ToColumn("PAR_DTHVENCIMENTO");
            Map(x => x.Status).ToColumn("PAR_STATUS");
            Map(x => x.ValorPago).ToColumn("PAR_VALORPAGO");
            Map(x => x.DataPagamento).ToColumn("PAR_DATAPAGAMENTO");

            Map(x => x.DataCadastro).ToColumn("PAR_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("PAR_DTHALTERACAO");

            Map(x => x.IdContrato).ToColumn("CON_CODIGO");
            Map(x => x.IdUsuarioCadastro).ToColumn("USU_CADASTRO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_ALTERACAO");

            Map(x => x.Contrato).Ignore();
            Map(x => x.UsuarioCadastro).Ignore();
            Map(x => x.UsuarioAlteracao).Ignore();
        }
    }
}