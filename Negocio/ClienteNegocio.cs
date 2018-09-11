using Dados;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;
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
        }
    }
}
