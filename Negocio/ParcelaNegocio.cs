using Dados;
using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projecao;
using Utilitario;
using Enumerador;

namespace Negocio
{
    public class ParcelaNegocio : NegocioBase<ParcelasEntidade>
    {
        public IEnumerable<ParcelasEntidade> ListarTodosCompleto()
        {
            return new ParcelaDados().ListarTodosCompleto();
        }

        public ParcelasEntidade ListarCompleto(long aCodigo)
        {
            return new ParcelaDados().ListarCompleto(aCodigo);
        }

        public IEnumerable<ParcelasEntidade> ParcelasPorContrato(int id)
        {
            return new ParcelaDados().ParcelasPorContrato(id);
        }

        public IEnumerable<ParcelasPorContratoProjecao> EntidadeParaProjecaoRelatorio(IEnumerable<ParcelasEntidade> aListaEntidade)
        {
            return aListaEntidade.Select(x => new ParcelasPorContratoProjecao()
            {
                Valor = StringUtilitario.ValorReais(x.Valor),
                DataPagamento = x.DataPagamento.HasValue ? x.DataPagamento.Value.ToString("dd/MM/yyyy") : null,
                Status = TraduzirStatus(x.Status),
                ValorPago = x.ValorPago.HasValue ? StringUtilitario.ValorReais(x.ValorPago.Value) : null,
                Vencimento = x.Vencimento.ToString("dd/MM/yyyy")
            });
        }

        private string TraduzirStatus(StatusParcelaEnum aStatus)
        {
            switch (aStatus)
            {
                case StatusParcelaEnum.Pendente:
                    return "Pendente";
                case StatusParcelaEnum.Liquidada:
                    return "Liquidada";
                case StatusParcelaEnum.Cancelada:
                    return "Cancelada";
                default:
                    return null;
            }
        }
    }
}
