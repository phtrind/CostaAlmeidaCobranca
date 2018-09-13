using Dados;
using Entidade;
using System;
using System.Collections.Generic;

namespace Negocio
{
    public class FinanceiroNegocio : NegocioBase<FinanceiroEntidade>
    {
        public override IEnumerable<FinanceiroEntidade> ListarTodos()
        {
            return new FinanceiroDados().ListarTodos();
        }

        public override FinanceiroEntidade Listar(long aCodigo)
        {
            return new FinanceiroDados().Listar(aCodigo);
        }

        public override void ValidateRegister(FinanceiroEntidade aEntidade, bool isEdicao)
        {
            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("Valor da transação inválido");
            }

            if (aEntidade.Valor == default(decimal))
            {
                throw new Exception("Valor da transação inválido");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue && !isEdicao)
            {
                throw new Exception("É obrigatório informar o usuário resposável pelo cadastro.");
            }
        }
    }
}
