using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Utilitario;

namespace Negocio
{
    public class ParcelaNegocio : NegocioBase<ParcelasEntidade>
    {
        #region .: Relatórios :.

        public IEnumerable<RelatorioParcelaPorContrato> RelatorioPorContrato(long aId)
        {
            return new ParcelaDados().ParcelasPorContrato(aId).Select(x => new RelatorioParcelaPorContrato()
            {
                Id = Convert.ToString(x.PAR_CODIGO),
                Valor = StringUtilitario.ValorReais(Convert.ToDecimal(x.PAR_VALOR)),
                Vencimento = Convert.ToDateTime(x.PAR_DTHVENCIMENTO).ToString("dd/MM/yyyy"),
                Status = StringUtilitario.TraduzirEnum((StatusParcela)x.PAR_STATUS),
                ValorPago = x.PAR_VALORPAGO != null ?
                                StringUtilitario.ValorReais(Convert.ToDecimal(x.PAR_VALORPAGO)) :
                                string.Empty,
                DataPagamento = x.PAR_DATAPAGAMENTO != null ?
                                    Convert.ToDateTime(x.PAR_DATAPAGAMENTO).ToString("dd/MM/yyyy") :
                                    string.Empty
            });
        }

        #endregion

        #region .: Cadastro / Edição :.

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
                StatusParcelas = EnumUtilitario.ConverterParaCombo(typeof(StatusParcela)).ToList()
            };
        }

        private void ValidarEdicao(ParcelasEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração da parcela não foi informado.");
            }

            if (!aEntidade.Id.HasValue)
            {
                throw new Exception("O Id da parcela não foi informada.");
            }

            if (new ParcelaDados().Listar(aEntidade.Id.Value) == null)
            {
                throw new Exception("A parcela informada não foi encontrada.");
            }

            if (aEntidade.Status == StatusParcela.Liquidada)
            {
                if (!aEntidade.ValorPago.HasValue)
                {
                    throw new Exception("É obrigatório informar o valor pago para parcela liquidada.");
                }

                if (!aEntidade.DataPagamento.HasValue)
                {
                    throw new Exception("É obrigatório informar a data do pagamento para parcela liquidada.");
                }
            }
        }

        public bool Editar(ParcelasEntidade aEntidade)
        {
            ValidarEdicao(aEntidade);

            var parcelaAntiga = Listar(aEntidade.Id.Value);

            aEntidade.IdContrato = parcelaAntiga.IdContrato.Value;
            aEntidade.IdUsuarioCadastro = parcelaAntiga.IdUsuarioCadastro.Value;
            aEntidade.DataCadastro = parcelaAntiga.DataCadastro;
            aEntidade.DataAlteracao = DateTime.Now;

            if (aEntidade.Status == StatusParcela.Cancelada || aEntidade.Status == StatusParcela.Pendente)
            {
                aEntidade.ValorPago = null;
                aEntidade.DataPagamento = null;
            }

            using (var transaction = new TransactionScope())
            {
                Atualizar(aEntidade);

                new ContratoNegocio().AtualizarValorTotal(parcelaAntiga.IdContrato.Value);

                transaction.Complete();
            }

            return true;
        }

        #endregion

        #region .: Exclusão :.

        public bool Deletar(long aId)
        {
            ValidarExclusao(aId);

            var parcela = Listar(aId);

            using (var transaction = new TransactionScope())
            {
                Excluir(parcela);

                var negocioContrato = new ContratoNegocio();

                var contrato = negocioContrato.Listar(parcela.IdContrato.Value);

                contrato.Valor -= parcela.Valor;

                contrato.Parcelas = ListarTodos().Where(x => x.IdContrato == contrato.Id.Value).ToList();

                negocioContrato.Atualizar(contrato);

                transaction.Complete();
            }

            return true;
        }

        private void ValidarExclusao(long aId)
        {
            var entidade = Listar(aId);

            if (entidade == null)
            {
                throw new Exception("Parcela não encontrada.");
            }

            if (!entidade.IdContrato.HasValue)
            {
                throw new Exception("Contrato não encontrada.");
            }

            var contrato = new ContratoNegocio().Listar(entidade.IdContrato.Value);

            if (contrato == null)
            {
                throw new Exception("Contrato não encontrado.");
            }

            if (RelatorioPorContrato(entidade.IdContrato.Value).Count() == 1)
            {
                throw new Exception("Não é possível excluir a única parcela do contrato.");
            }
        }

        #endregion
    }
}
