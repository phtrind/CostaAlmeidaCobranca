using Entidade;
using System;

namespace Negocio
{
    public class EnderecoNegocio : NegocioBase<EnderecoEntidade>
    {
        public override void ValidateRegister(EnderecoEntidade aEntidade)
        {
            if (string.IsNullOrEmpty(aEntidade.Logradouro))
            {
                throw new Exception("É obrigatório informar o logradouro do endereço.");
            }

            if (string.IsNullOrEmpty(aEntidade.Numero))
            {
                throw new Exception("É obrigatório informar o número do endereço.");
            }

            if (string.IsNullOrEmpty(aEntidade.Bairro))
            {
                throw new Exception("É obrigatório informar o bairro do endereço.");
            }

            if (aEntidade.Cep.ToString().Length != 8)
            {
                throw new Exception("O CEP do endereço informado é inválido.");
            }

            if (string.IsNullOrEmpty(aEntidade.Cidade))
            {
                throw new Exception("É obrigatório informar a cidade do endereço.");
            }

            if (string.IsNullOrEmpty(aEntidade.Estado))
            {
                throw new Exception("É obrigatório informar o estado do endereço.");
            }

            if (!aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("É obrigatório informar o usuário responsável pelo cadastro.");
            }
        }
    }
}
