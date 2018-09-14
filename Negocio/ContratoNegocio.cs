using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Linq;
using System.Transactions;

namespace Negocio
{
    public class ContratoNegocio : NegocioBase<ContratoEntidade>
    {
        public GetCombosCadastroContratoResponse getCombosCadastroContrato()
        {
            return new GetCombosCadastroContratoResponse()
            {
                Clientes = new ClienteNegocio().getComboClientes(),
                Eventos = new EventoNegocio().getComboEventos()
            };
        }

        public string TraduzirStatus(StatusContratoEnum status)
        {
            switch (status)
            {
                case StatusContratoEnum.Ativo:
                    return "Ativo";
                case StatusContratoEnum.Suspenso:
                    return "Suspenso";
                case StatusContratoEnum.Cancelado:
                    return "Cancelado";
                default:
                    return string.Empty;
            }
        }

        public long Cadastrar(ContratoEntidade aEntidade)
        {
            using (var transation = new TransactionScope())
            {
                #region .: Contrato :.

                aEntidade.DataCadastro = DateTime.Now;

                var idContrato = Inserir(aEntidade);

                #endregion

                #region .: Parcelas :.

                foreach (var parcela in aEntidade.Parcelas)
                {
                    parcela.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                    parcela.IdContrato = idContrato;
                    parcela.DataCadastro = DateTime.Now;
                    parcela.Status = StatusParcelaEnum.Pendente;

                    new ParcelaNegocio().Inserir(parcela);
                }

                #endregion

                transation.Complete();

                return idContrato;
            }
        }

        public override void ValidateRegister(ContratoEntidade aEntidade, bool isEdicao)
        {
            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("O valor do contrato é inválido.");
            }

            if (aEntidade.Parcelas == null || !aEntidade.Parcelas.Any())
            {
                throw new Exception("Não é possível cadastrar um contrato sem parcelas.");
            }

            if (aEntidade.Valor != aEntidade.Parcelas.Sum(x => x.Valor))
            {
                throw new Exception("O valor do contrato não pode ser diferente da soma do valor das parcelas");
            }

            if (string.IsNullOrEmpty(aEntidade.Animal))
            {
                throw new Exception("É obrigatório informar o animal do contrato.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue && !isEdicao)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }

            if (!aEntidade.IdVendedor.HasValue)
            {
                throw new Exception("É obrigatório infomar o vendedor do contrato.");
            }

            if (!aEntidade.IdComprador.HasValue)
            {
                throw new Exception("É obrigatório infomar o comprador do contrato.");
            }
        }

        public bool Editar(ContratoEntidade aEntidade)
        {
            ValidarEdicao(aEntidade);

            var contrato = new ContratoDados().Listar(aEntidade.Id.Value);

            aEntidade.IdUsuarioCadastro = contrato.IdUsuarioCadastro.Value;
            aEntidade.IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao.Value;
            aEntidade.DataCadastro = contrato.DataCadastro;
            aEntidade.DataAlteracao = DateTime.Now;
            aEntidade.Status = contrato.Status;

            using (var transation = new TransactionScope())
            {
                var negocioParcela = new ParcelaNegocio();

                foreach (var parcela in aEntidade.Parcelas)
                {
                    var parcelaAntiga = negocioParcela.Listar(parcela.Id.Value);

                    if (parcelaAntiga != null)
                    {
                        parcela.IdUsuarioCadastro = parcelaAntiga.IdUsuarioCadastro.Value;
                        parcela.IdUsuarioAlteracao = contrato.IdUsuarioAlteracao.Value;
                        parcela.DataCadastro = parcelaAntiga.DataCadastro;
                        parcela.DataAlteracao = DateTime.Now;
                        parcela.ValorPago = parcelaAntiga.ValorPago;
                        parcela.DataPagamento = parcelaAntiga.DataPagamento;

                        negocioParcela.Atualizar(parcela);
                    }
                    else
                    {
                        parcela.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                        parcela.IdContrato = aEntidade.Id;
                        parcela.DataCadastro = DateTime.Now;
                        parcela.Status = StatusParcelaEnum.Pendente;

                        negocioParcela.Inserir(parcela);
                    }
                }

                Atualizar(aEntidade);

                transation.Complete();
            }

            return true;
        }

        private void ValidarEdicao(ContratoEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração do cadastro do contrato não foi informado.");
            }

            if (!aEntidade.Id.HasValue)
            {
                throw new Exception("O Id do contrato não foi informado.");
            }

            if (new ContratoDados().Listar(aEntidade.Id.Value) == null)
            {
                throw new Exception("O contrato informado não foi encontrado.");
            }
        }

        //public IEnumerable<RelatorioContratoResponse> Relatorio()
        //{
        //    dynamic dados = new ContratoDados().Relatorio();
        //}
    }
}
