using Dados;
using Entidade;
using System;

namespace Negocio
{
    public class EnderecoNegocio : NegocioBase<EnderecoEntidade>
    {
        public override void ValidateRegister(EnderecoEntidade aEntidade, bool isEdicao)
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

            if (!isEdicao && !aEntidade.IdUsuarioCadastro.HasValue)
            {
                throw new Exception("É obrigatório informar o usuário responsável pelo cadastro.");
            }
        }

        public void ValidarEdicao(EnderecoEntidade aEntidade)
        {
            if (!aEntidade.IdUsuarioAlteracao.HasValue)
            {
                throw new Exception("O usuário responsável pela alteração do cadastro do endereço não foi informado.");
            }

            if (!aEntidade.Id.HasValue)
            {
                throw new Exception("O Id do endereço não foi informado.");
            }

            if (new EnderecoDados().Listar(aEntidade.Id.Value) == null)
            {
                throw new Exception("O endreço informado não foi encontrado.");
            }
        }
    }
}
