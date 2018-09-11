using Dapper.FluentMap.Dommel.Mapping;
using Entidade;

namespace Dados.Mapeamento
{
    public class UsuarioMap : DommelEntityMap<UsuarioEntidade>
    {
        public UsuarioMap()
        {
            ToTable("USU_USUARIOS");

            Map(x => x.Id).ToColumn("USU_CODIGO").IsKey().IsIdentity();

            Map(x => x.Email).ToColumn("USU_EMAIL");
            Map(x => x.Senha).ToColumn("USU_SENHA");
            Map(x => x.Tipo).ToColumn("USU_TIPO");

            Map(x => x.DataCadastro).ToColumn("USU_DTHCADASTRO");
            Map(x => x.DataAlteracao).ToColumn("USU_DTHALTERACAO");

            Map(x => x.IdUsuarioCadastro).ToColumn("USU_CADASTRO");
            Map(x => x.IdUsuarioAlteracao).ToColumn("USU_ALTERACAO");

            Map(x => x.UsuarioCadastro).Ignore();
            Map(x => x.UsuarioAlteracao).Ignore();
        }
    }
}
