using Dapper;
using Entidade;
using System;
using System.Collections.Generic;

namespace Dados
{
    public class ParcelaDados : DadosBase<ParcelasEntidade>
    {
        public IEnumerable<dynamic> ParcelasPorContrato(long aCodigo)
        {
            return db.Query($@"SELECT PAR_CODIGO, 
                                      PAR_VALOR, 
                                      PAR_DTHVENCIMENTO, 
                                      PAR_STATUS, 
                                      PAR_VALORPAGO, 
                                      PAR_DATAPAGAMENTO
                               FROM PAR_PARCELAS
                               WHERE PAR_PARCELAS.CON_CODIGO = {aCodigo} ");
        }

        public dynamic BuscarParaEditar(long aCodigo)
        {
            return db.QueryFirstOrDefault($@"SELECT PAR_CODIGO, 
                                                     PAR_VALOR, 
	                                                 PAR_TAXALUCRO,
                                                     PAR_DTHVENCIMENTO, 
                                                     PAR_STATUS, 
                                                     PAR_VALORPAGO, 
                                                     PAR_DATAPAGAMENTO
                                             FROM PAR_PARCELAS
                                             WHERE PAR_CODIGO = {aCodigo} ");
        }
    }
}
