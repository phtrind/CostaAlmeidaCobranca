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
    public class ClienteNegocio : NegocioBase<ClienteEntidade>
    {
        public List<ComboProjecao> getComboClientes()
        {
            return new ClienteDados().getComboClientes().Select(x => new ComboProjecao()
            {
                Codigo = Convert.ToInt64(x.CLI_CODIGO),
                Descricao = x.CLI_NOME
            }).ToList();
        }

        public override void ValidateRegister(ClienteEntidade aEntidade, bool isEdicao)
        {
            if (string.IsNullOrEmpty(aEntidade.Nome))
            {
                throw new Exception("O nome do cliente não foi informado.");
            }

            if (!StringUtilitario.CpfIsValid(aEntidade.Cpf))
            {
                throw new Exception("O CPF do cliente informado é inválido.");
            }

            if (!StringUtilitario.EmailIsValid(aEntidade.Email))
            {
                throw new Exception("O e-mail do cliente informado é inválido.");
            }

            if (!isEdicao)
            {
                if (!aEntidade.IdUsuarioCadastro.HasValue)
                {
                    throw new Exception("O usuário responsável pelo cadastro do cliente não foi informado.");
                }

                VerificarChaves(aEntidade);
            }
        }

        private void VerificarChaves(ClienteEntidade aEntidade)
        {
            var dados = new ClienteDados();

            if (dados.BuscarClientePeloEmail(aEntidade.Email) != null)
            {
                throw new Exception("Já existe um cliente cadastrado com esse e-mail.");
            }

            if (dados.BuscarClientePeloCpfCnpj(aEntidade.Cpf) != null)
            {
                throw new Exception("Já existe um cliente cadastrado com esse CPF/CNPJ.");
            }
        }

        public long Cadastrar(ClienteEntidade aEntidade)
        {
            using (var transation = new TransactionScope())
            {
                #region .: Usuário :.

                var usuario = new UsuarioEntidade()
                {
                    DataCadastro = DateTime.Now,
                    Email = aEntidade.Email,
                    Senha = StringUtilitario.GerarSenhaAlatoria(),
                    Tipo = TipoUsuarioEnum.Cliente,
                    IdUsuarioCadastro = aEntidade.IdUsuarioCadastro
                };

                aEntidade.IdUsuario = new UsuarioNegocio().Inserir(usuario);

                #endregion

                #region .: Endereço :.

                aEntidade.Endereco.IdUsuarioCadastro = aEntidade.IdUsuarioCadastro;
                aEntidade.Endereco.DataCadastro = DateTime.Now;

                aEntidade.IdEndereco = new EnderecoNegocio().Inserir(aEntidade.Endereco);

                #endregion

                #region .: Cliente :.

                aEntidade.DataCadastro = DateTime.Now;

                var codCliente = Inserir(aEntidade);

                #endregion

                transation.Complete();

                return codCliente;
            }
        }

        #region .: Edição :.

        public bool Editar(ClienteEntidade aEntidade)
        {
            ValidarEdicao(aEntidade);

            var cliente = new ClienteDados().Listar(aEntidade.Id.Value);

            aEntidade.IdUsuario = cliente.IdUsuario.Value;
            aEntidade.IdUsuarioCadastro = cliente.IdUsuarioCadastro.Value;
            aEntidade.IdUsuarioAlteracao = aEntidade.IdUsuarioAlteracao.Value;
            aEntidade.IdEndereco = cliente.IdEndereco;
            aEntidade.DataCadastro = cliente.DataCadastro;
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

        private void ValidarEdicao(ClienteEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração do cadastro do cliente não foi informado.");
            }

            if (!aEntidade.Id.HasValue)
            {
                throw new Exception("O Id do cliente não foi informado.");
            }

            if (new ClienteDados().Listar(aEntidade.Id.Value) == null)
            {
                throw new Exception("O cliente informado não foi encontrado.");
            }
        } 

        #endregion

        #region .: Relatório :.

        public IEnumerable<RelatorioClienteResponse> Relatorio()
        {
            return new ClienteDados().ListarTodos().Select(x => new RelatorioClienteResponse()
            {
                Id = x.Id.Value,
                Nome = x.Nome,
                Cpf = x.Cpf,
                Email = x.Email,
                Telefone = string.IsNullOrEmpty(x.TelefoneFixo) ? string.Empty : x.TelefoneFixo,
                Celular = string.IsNullOrEmpty(x.TelefoneCelular) ? string.Empty : x.TelefoneCelular,
                Fazenda = string.IsNullOrEmpty(x.Fazenda) ? string.Empty : x.Fazenda
            });
        }

        public RelatorioDetalhadoClienteResponse RelatorioDetalhado(long idCliente)
        {
            var dados = new ClienteDados().RelatorioDetalhado(idCliente);

            if (dados == null)
            {
                throw new Exception("Cliente não encontrado.");
            }

            var cliente = new RelatorioDetalhadoClienteResponse
            {
                IdCliente = dados.CLI_CODIGO,
                Nome = dados.CLI_NOME,
                Email = dados.CLI_EMAIL,
                Fazenda = StringUtilitario.VerificarStringNula(dados.CLI_FAZENDA),
                Cpf = dados.CLI_CPF,
                Telefone = StringUtilitario.VerificarStringNula(dados.CLI_TELFIXO),
                Celular = StringUtilitario.VerificarStringNula(dados.CLI_TELCELULAR),
                IdEndereco = dados.END_CODIGO,
                Cep = Convert.ToString(dados.END_CEP),
                Logradouro = dados.END_LOGRADOURO,
                Numero = dados.END_NUMERO,
                Complemento = StringUtilitario.VerificarStringNula(dados.END_COMPLEMENTO),
                Bairro = dados.END_BAIRRO,
                Estado = dados.END_ESTADO,
                Cidade = dados.END_CIDADE
            };

            cliente.TipoDocumento = cliente.Cpf.Length == 11 ? "CPF" : "CNPJ";

            return cliente;
        }

        #endregion
    }
}
