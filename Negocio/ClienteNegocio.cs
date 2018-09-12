using Dados;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilitario;

namespace Negocio
{
    public class ClienteNegocio : NegocioBase<ClienteEntidade>
    {
        public IEnumerable<ComboProjecao> getComboClientes()
        {
            return new ClienteDados().getComboClientes();
        }

        public override void ValidateRegister(ClienteEntidade aEntidade)
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

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("O usuário responsável pelo cadastro do cliente não foi informado.");
            }

            VerificarChaves(aEntidade);
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
    }
}
