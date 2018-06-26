using Dapper.FluentMap.Dommel.Mapping;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados.Mapeamento
{
    public class EventoMap : DommelEntityMap<EventoEntidade>
    {
        public EventoMap()
        {
            ToTable("EVE_EVENTOS");

            Map(x => x.Id).ToColumn("EVE_CODIGO").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("EVE_NOME");
            Map(x => x.Data).ToColumn("EVE_DATA");

            Map(x => x.IdEndereco).ToColumn("END_CODIGO");
            Map(x => x.IdUsuario).ToColumn("USU_CODIGO");

            Map(x => x.DataCadastro).ToColumn("EVE_DTHCADASTRO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Endereco).Ignore();
            Map(x => x.Contratos).Ignore();
        }
    }
}
