using Dapper;
using Entidade;
using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dados
{
    public class ParcelaDados : DadosBase<ParcelasEntidade>
    {
        public IEnumerable<ParcelasEntidade> ParcelasPorContrato(int aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM PAR_PARCELAS 
                                         WHERE PAR_PARCELAS.CON_CODIGO = {aCodigo} ");

            return DadosParaEntidadeBasico(resultado);
        }

        private IEnumerable<ParcelasEntidade> DadosParaEntidadeBasico(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ParcelasEntidade()
            {
                Id = Convert.ToInt64(x.PAR_CODIGO),
                Valor = Convert.ToDecimal(x.PAR_VALOR),
                Vencimento = x.PAR_DTHVENCIMENTO,
                TaxaLucro = Convert.ToDecimal(x.PAR_TAXALUCRO),
                Status = (StatusParcelaEnum)Convert.ToInt32(x.PAR_STATUS),
                ValorPago = x.PAR_VALORPAGO != null ? Convert.ToDecimal(x.PAR_VALORPAGO) : null,
                DataPagamento = x.PAR_DATAPAGAMENTO != null ? Convert.ToDateTime(x.PAR_DATAPAGAMENTO) : null,
                IdContrato = Convert.ToInt64(x.CON_CODIGO),
                DataCadastro = Convert.ToDateTime(x.PAR_DTHCADASTRO),
                DataAlteracao = Convert.ToDateTime(x.PAR_DTHALTERACAO)
            });
        }
    }
}
