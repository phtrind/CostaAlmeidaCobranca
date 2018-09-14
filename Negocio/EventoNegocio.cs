using Dados;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Utilitario;

namespace Negocio
{
    public class EventoNegocio : NegocioBase<EventoEntidade>
    {
        public override IEnumerable<EventoEntidade> ListarTodos()
        {
            return new EventoDados().ListarTodos();
        }

        public override EventoEntidade Listar(long aCodigo)
        {
            return new EventoDados().Listar(aCodigo);
        }

        public List<ComboProjecao> getComboEventos()
        {
            return new EventoDados().getComboEventos().Select(x => new ComboProjecao()
            {
                Codigo = Convert.ToInt64(x.EVE_CODIGO),
                Descricao = x.EVE_NOME
            }).ToList();
        }

        public override void ValidateRegister(EventoEntidade aEntidade, bool isEdicao)
        {
            if (string.IsNullOrEmpty(aEntidade.Nome))
            {
                throw new Exception("É obrigatório informar o nome do evento.");
            }

            if (aEntidade.Data == default(DateTime))
            {
                throw new Exception("A data do evento informada é inválida.");
            }

            if (!isEdicao)
            {
                if (!aEntidade.IdUsuarioCadastro.HasValue)
                {
                    throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
                }

                VerificarChaves(aEntidade);
            }
        }

        public long Cadastrar(EventoEntidade aEntidade)
        {
            using (var transation = new TransactionScope())
            {
                #region .: Endereço :.

                aEntidade.Endereco.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                aEntidade.Endereco.DataCadastro = DateTime.Now;

                aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

                #endregion

                #region .: Evento :.

                aEntidade.DataCadastro = DateTime.Now;

                var codEndereco = Inserir(aEntidade);

                #endregion

                transation.Complete();

                return codEndereco;
            }
        }

        private void VerificarChaves(EventoEntidade aEntidade)
        {
            if (!new EventoDados().ValidarChaves(aEntidade))
            {
                throw new Exception("Já existe um evento com esse nome e essa data cadastrado.");
            }
        }

        #region .: Edição :.

        public bool Editar(EventoEntidade aEntidade)
        {
            ValidarEdicao(aEntidade);

            var evento = new EventoDados().Listar(aEntidade.Id.Value);

            aEntidade.IdUsuarioCadastro = evento.IdUsuarioCadastro.Value;
            aEntidade.IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao.Value;
            aEntidade.IdEndereco = evento.IdEndereco;
            aEntidade.DataCadastro = evento.DataCadastro;
            aEntidade.DataAlteracao = DateTime.Now;

            var negocioEndereco = new EnderecoNegocio();

            negocioEndereco.ValidarEdicao(aEntidade.Endereco);

            var endereco = negocioEndereco.Listar(aEntidade.Endereco.Id.Value);

            aEntidade.Endereco.IdUsuarioCadastro = endereco.IdUsuarioCadastro.Value;
            aEntidade.Endereco.IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao.Value;
            aEntidade.Endereco.DataCadastro = endereco.DataCadastro;
            aEntidade.Endereco.DataAlteracao = DateTime.Now;

            using (var transation = new TransactionScope())
            {
                negocioEndereco.Atualizar(aEntidade.Endereco);

                Atualizar(aEntidade);

                transation.Complete();
            }

            return true;
        }

        private void ValidarEdicao(EventoEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração do cadastro do evento não foi informado.");
            }

            if (!aEntidade.Id.HasValue)
            {
                throw new Exception("O Id do evento não foi informado.");
            }

            if (new ClienteDados().Listar(aEntidade.Id.Value) == null)
            {
                throw new Exception("O evento informado não foi encontrado.");
            }
        } 

        #endregion

        #region .: Relatório :.

        public IEnumerable<RelatorioEventoResponse> Relatorio()
        {
            return new EventoDados().Relatorio().Select(x => new RelatorioEventoResponse()
            {
                Id = Convert.ToInt64(x.EVE_CODIGO),
                Nome = x.EVE_NOME,
                Data = StringUtilitario.TratarDataTime(x.EVE_DATA),
                Localidade = x.LOCALIDADE
            });
        }

        public RelatorioDetalhadoEventoResponse RelatorioDetalhado(long aIdEvento)
        {
            var dados = new EventoDados().RelatorioDetalhado(aIdEvento);

            if (dados == null)
            {
                throw new Exception("Leilão não encontrado");
            }

            return new RelatorioDetalhadoEventoResponse()
            {
                IdEvento = dados.EVE_CODIGO,
                Nome = dados.EVE_NOME,
                Data = StringUtilitario.TratarDataTime(dados.EVE_DATA),
                DataDateTime = Convert.ToDateTime(dados.EVE_DATA),
                IdEndereco = dados.END_CODIGO,
                Cep = Convert.ToString(dados.END_CEP),
                Logradouro = dados.END_LOGRADOURO,
                Numero = dados.END_NUMERO,
                Complemento = StringUtilitario.VerificarStringNula(dados.END_COMPLEMENTO),
                Bairro = dados.END_BAIRRO,
                Estado = dados.END_ESTADO,
                Cidade = dados.END_CIDADE,
            };
        }

        #endregion
    }
}
