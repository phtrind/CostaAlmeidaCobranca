using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
