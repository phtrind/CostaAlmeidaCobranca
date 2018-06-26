﻿using Dapper.FluentMap.Dommel.Mapping;
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
            ToTable("PAR_PARCELAS");

            Map(x => x.Id).ToColumn("PAR_CODIGO").IsKey().IsIdentity();

            Map(x => x.DataCadastro).ToColumn("PAR_DTHCADASTRO");

            Map(x => x.Valor).ToColumn("PAR_VALOR");
            Map(x => x.Vencimento).ToColumn("PAR_DTHVENCIMENTO");
            Map(x => x.Status).ToColumn("PAR_STATUS");
            Map(x => x.Juros).ToColumn("PAR_JUROS");

            Map(x => x.IdContrato).ToColumn("CON_CODIGO");

            Map(x => x.Contrato).Ignore();
        }
    }
}
