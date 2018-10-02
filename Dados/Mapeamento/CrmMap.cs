using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class CrmMap : DommelEntityMap<CrmEntidade>
    {
        public CrmMap()
        {
            ToTable("CRM_CRMCONTRATOS");

            Map(x => x.Id).ToColumn("CRM_CODIGO").IsKey().IsIdentity();

            Map(x => x.Descricao).ToColumn("CRM_DESCRICAO");

            Map(x => x.DataCadastro).ToColumn("CRM_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("CRM_DTHALTERACAO");

            Map(x => x.IdContrato).ToColumn("CON_CODIGO");
            Map(x => x.IdUsuarioCadastro).ToColumn("USU_CADASTRO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_ALTERACAO");

            Map(x => x.Contrato).Ignore();
            Map(x => x.UsuarioCadastro).Ignore();
            Map(x => x.UsuarioAlteracao).Ignore();
        }
    }
}
