using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilitario;

namespace Negocio
{
    public class ParcelaNegocio : NegocioBase<ParcelasEntidade>
    {
        public IEnumerable<RelatorioParcelaPorContrato> RelatorioPorContrato(int aId)
        {
            return new ParcelaDados().ParcelasPorContrato(aId).Select(x => new RelatorioParcelaPorContrato()
            {
                Id = Convert.ToString(x.PAR_CODIGO),
                Valor = StringUtilitario.ValorReais(Convert.ToDecimal(x.PAR_VALOR)),
                Vencimento = Convert.ToDateTime(x.PAR_DTHVENCIMENTO).ToString("dd/MM/yyyy"),
                Status = StringUtilitario.TraduzirEnum((StatusParcelaEnum)x.PAR_STATUS),
                ValorPago = x.PAR_VALORPAGO != null ?
                                StringUtilitario.ValorReais(Convert.ToDecimal(x.PAR_VALORPAGO)) :
                                string.Empty,
                DataPagamento = x.PAR_DATAPAGAMENTO != null ?
                                    Convert.ToDateTime(x.PAR_DATAPAGAMENTO).ToString("dd/MM/yyyy") :
                                    string.Empty
            });
        }

        public BuscarParaEditarParcelaResponse BuscarParaEditar(int aId)
        {
            var parcela = new ParcelaDados().BuscarParaEditar(aId);

            return new BuscarParaEditarParcelaResponse
            {
                Id = Convert.ToInt64(parcela.PAR_CODIGO),
                Valor = Convert.ToDecimal(parcela.PAR_VALOR),
                TaxaLucro = Convert.ToDecimal(parcela.PAR_TAXALUCRO),
                DataVencimento = Convert.ToDateTime(parcela.PAR_DTHVENCIMENTO),
                Status = Convert.ToInt32(parcela.PAR_STATUS),
                ValorPago = parcela.PAR_VALORPAGO != null ?
                            Convert.ToDecimal(parcela.PAR_VALORPAGO) :
                            null,
                DataPagamento = parcela.PAR_DATAPAGAMENTO != null ?
                                Convert.ToDateTime(parcela.PAR_DATAPAGAMENTO) :
                                null,
                StatusParcelas = EnumUtilitario.ConverterParaCombo(typeof(StatusParcelaEnum)).ToList()
            };
        }

        public override void ValidateRegister(ParcelasEntidade aEntidade, bool isEdicao)
        {
            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("O valor da parcela é inválido.");
            }

            if (aEntidade.Vencimento == default(DateTime))
            {
                throw new Exception("A data de vencimento da parcela é inválida.");
            }

            if (!aEntidade.IdContrato.HasValue)
            {
                throw new Exception("É obrigatório informar o contrato relacionado à essa parcela.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue && !isEdicao)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }
        }
    }
}
