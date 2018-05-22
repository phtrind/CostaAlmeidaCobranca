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
            ToTable("EVENTOS");

            Map(x => x.Id).ToColumn("COD_CODEVENTO").IsKey().IsIdentity();
            Map(x => x.Nome).ToColumn("TXT_NOMEEVENTO");
            Map(x => x.Data).ToColumn("DTH_DATAEVENTO");

            Map(x => x.IdEndereco).ToColumn("COD_CODENDERECO");
            Map(x => x.IdUsuario).ToColumn("COD_CODUSUARIO");

            Map(x => x.DataCadastro).ToColumn("DTH_CADASTROEVENTO");

            Map(x => x.Usuario).Ignore();
            Map(x => x.Endereco).Ignore();
            Map(x => x.Contratos).Ignore();
        }
    }
}
