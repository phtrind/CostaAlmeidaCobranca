using Dapper;
using Entidade;
using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ParcelaDados : DadosBase<ParcelasEntidade>
    {
        public IEnumerable<ParcelasEntidade> ListarTodosCompleto()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM PAR_PARCELAS
                                            INNER JOIN CON_CONTRATOS
                                            ON PAR_PARCELAS.CON_CODIGO = CON_CONTRATOS.CON_CODIGO ");

            return DadosParaEntidadeCompleto(resultado);
        }

        public IEnumerable<ParcelasEntidade> ParcelasPorContrato(int aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM PAR_PARCELAS 
                                         WHERE PAR_PARCELAS.CON_CODIGO = {aCodigo} ");

            return DadosParaEntidadeBasico(resultado);
        }

        public ParcelasEntidade ListarCompleto(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM PAR_PARCELAS
                                             INNER JOIN CON_CONTRATOS
                                             ON PAR_PARCELAS.CON_CODIGO = CON_CONTRATOS.CON_CODIGO 
                                         WHERE PAR_PARCELAS.PAR_CODIGO = {aCodigo} ");

            return DadosParaEntidadeCompleto(resultado).FirstOrDefault();
        }

        private IEnumerable<ParcelasEntidade> DadosParaEntidadeCompleto(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ParcelasEntidade()
            {
                Id = Convert.ToInt64(x.PAR_CODIGO),
                Valor = Convert.ToDecimal(x.PAR_VALOR),
                Vencimento = x.PAR_DTHVENCIMENTO,
                Status = (StatusParcelaEnum)Convert.ToInt32(x.PAR_STATUS),
                ValorPago = x.PAR_VALORPAGO != null ? Convert.ToDecimal(x.PAR_VALORPAGO) : null,
                DataPagamento = x.PAR_DATAPAGAMENTO != null ? Convert.ToDateTime(x.PAR_DATAPAGAMENTO) : null,
                IdContrato = Convert.ToInt64(x.CON_CODIGO),
                DataCadastro = Convert.ToDateTime(x.PAR_DTHCADASTRO),
                Contrato = new ContratoEntidade()
                {
                    Id = Convert.ToInt64(x.CON_CODIGO),
                    Valor = Convert.ToDecimal(x.CON_VALOR),
                    TaxaLucro = Convert.ToDecimal(x.CON_TAXALUCRO),
                    Observacao = x.CON_OBSERVACAO,
                    Status = (StatusContratoEnum)Convert.ToInt32(x.CON_STATUS),
                    IdUsuario = Convert.ToInt64(x.USU_CODIGO),
                    IdEvento = Convert.ToInt64(x.EVE_CODIGO),
                    IdVendedor = Convert.ToInt64(x.CON_VENDEDOR),
                    IdComprador = Convert.ToInt64(x.CON_COMPRADOR),
                    DataCadastro = x.CON_DTHCADASTRO
                }
            });
        }

        private IEnumerable<ParcelasEntidade> DadosParaEntidadeBasico(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ParcelasEntidade()
            {
                Id = Convert.ToInt64(x.PAR_CODIGO),
                Valor = Convert.ToDecimal(x.PAR_VALOR),
                Vencimento = x.PAR_DTHVENCIMENTO,
                Status = (StatusParcelaEnum)Convert.ToInt32(x.PAR_STATUS),
                ValorPago = x.PAR_VALORPAGO != null ? Convert.ToDecimal(x.PAR_VALORPAGO) : null,
                DataPagamento = x.PAR_DATAPAGAMENTO != null ? Convert.ToDateTime(x.PAR_DATAPAGAMENTO) : null,
                IdContrato = Convert.ToInt64(x.CON_CODIGO),
                DataCadastro = Convert.ToDateTime(x.PAR_DTHCADASTRO)
            });
        }
    }
}
