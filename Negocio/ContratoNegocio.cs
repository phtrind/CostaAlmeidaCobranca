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
    public class ContratoNegocio : NegocioBase<ContratoEntidade>
    {
        #region .: Busca :.

        public BuscarParaEditarContratoResponse BuscarParaEditar(long idContrato)
        {
            var dados = new ContratoDados().BuscarParaEditar(idContrato);

            if (!dados.Any())
            {
                throw new Exception("Contrato não encontrado.");
            }

            var contrato = dados.FirstOrDefault();

            var retorno = new BuscarParaEditarContratoResponse
            {
                Id = contrato.CON_CODIGO,
                IdVendedor = contrato.CON_VENDEDOR,
                IdComprador = contrato.CON_COMPRADOR,
                Animal = contrato.CON_ANIMAL,
                Observação = StringUtilitario.TratarStringNula(contrato.CON_OBSERVACAO),
                IdEvento = StringUtilitario.TratarStringNula(Convert.ToString(contrato.EVE_CODIGO)),
                Valor = Convert.ToDecimal(contrato.CON_VALOR),
                QuantidadeParcelas = dados.Count(),
                Parcelas = new List<BuscarParaEditarParcelaResponse>()
            };

            foreach (var par in dados)
            {
                retorno.Parcelas.Add(new BuscarParaEditarParcelaResponse
                {
                    Id = par.PAR_CODIGO,
                    Valor = Convert.ToDecimal(par.PAR_VALOR),
                    DataVencimento = Convert.ToDateTime(par.PAR_DTHVENCIMENTO),
                    TaxaLucro = Convert.ToDecimal(par.PAR_TAXALUCRO)
                });
            }

            return retorno;
        }

        public GetCombosCadastroContratoResponse getCombosCadastroContrato()
        {
            return new GetCombosCadastroContratoResponse()
            {
                Clientes = new ClienteNegocio().getComboClientes(),
                Eventos = new EventoNegocio().getComboEventos()
            };
        }

        #endregion

        #region .: Cadastro / Edição :.

        public long Cadastrar(ContratoEntidade aEntidade)
        {
            using (var transation = new TransactionScope())
            {
                #region .: Contrato :.

                aEntidade.DataCadastro = DateTime.Now;
                aEntidade.Status = StatusContrato.Ativo;

                var idContrato = Inserir(aEntidade);

                #endregion

                #region .: Parcelas :.

                foreach (var parcela in aEntidade.Parcelas)
                {
                    parcela.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                    parcela.IdContrato = idContrato;
                    parcela.DataCadastro = DateTime.Now;
                    parcela.Status = StatusParcela.Pendente;

                    new ParcelaNegocio().Inserir(parcela);
                }

                #endregion

                transation.Complete();

                return idContrato;
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
                    var parcelaAntiga = parcela.Id.HasValue ? negocioParcela.Listar(parcela.Id.Value) : null;

                    if (parcelaAntiga != null)
                    {
                        parcela.IdContrato = parcelaAntiga.IdContrato.Value;
                        parcela.IdUsuarioCadastro = parcelaAntiga.IdUsuarioCadastro.Value;
                        parcela.IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao.Value;
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
                        parcela.Status = StatusParcela.Pendente;

                        negocioParcela.Inserir(parcela);
                    }
                }

                Atualizar(aEntidade);

                transation.Complete();
            }

            return true;
        }

        public void AtualizarValorTotal(long aId)
        {
            new ContratoDados().AtualizarValorTotal(aId);
        }

        #endregion

        #region .: Validações :.

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

        private void ValidarEdicao(ContratoEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração do contrato não foi informado.");
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

        #endregion

        #region .: Relatórios :.

        public IEnumerable<RelatorioContratoResponse> Relatorio()
        {
            return new ContratoDados().Relatorio().Select(x => new RelatorioContratoResponse()
            {
                Id = x.CON_CODIGO,
                Vendedor = x.VENDEDOR,
                Comprador = x.COMPRADOR,
                Evento = x.EVE_NOME ?? string.Empty,
                Valor = StringUtilitario.ValorReais(Convert.ToDecimal(x.CON_VALOR)),
                Status = StringUtilitario.TraduzirEnum((StatusContrato)x.CON_STATUS),
                Parcelas = Convert.ToString(x.PARCELAS)
            });
        }

        #endregion
    }
}
