using Entidade;
using System;
using Utilitario;

namespace Negocio
{
    public class FuncionarioNegocio : NegocioBase<FuncionarioEntidade>
    {
        public override void ValidateRegister(FuncionarioEntidade aEntidade, bool isEdicao)
        {
            if (string.IsNullOrEmpty(aEntidade.Nome))
            {
                throw new Exception("É obrigatório inforar o nome do funcionário.");
            }

            if (!StringUtilitario.CpfIsValid(aEntidade.Cpf))
            {
                throw new Exception("O CPF do funcionário informado é inválido.");
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

        private void VerificarChaves(FuncionarioEntidade aEntidade)
        {
            //Necessário ainda implementar
        }
    }
}