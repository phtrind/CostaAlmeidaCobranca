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

        public override void ValidateRegister(ParcelasEntidade aEntidade)
        {
            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("O valor da parcela é inválido.");
            }

            if (aEntidade.Vencimento == default(DateTime))
            {
                throw new Exception("A data de vencimento da parcela é inválida.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }
        }
    }
}
