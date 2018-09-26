using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Linq;

namespace Negocio
{
    public class UsuarioNegocio : NegocioBase<UsuarioEntidade>
    {
        public bool VerificarLogin(string aEmail, string aSenha)
        {
            if (new UsuarioDados().BuscarLogin(aEmail, aSenha).Any())
            {
                return true;
            }

            return false;
        }

        public InformacoesUsuarioResponse InformacoesLogin(string aEmail)
        {
            var dados = new UsuarioDados();

            var usuario = dados.BuscarUsuarioPeloEmail(aEmail);

            switch (usuario.Tipo)
            {
                case TipoUsuario.Funcionario:
                    return dados.BuscarInfoLoginFuncionario(usuario.Id);
                case TipoUsuario.Cliente:
                    return dados.BuscarInfoLoginCliente(usuario.Id);
                default:
                    return null;
            }
        }

        public override void ValidateRegister(UsuarioEntidade aEntidade, bool isEdicao)
        {
            if (string.IsNullOrEmpty(aEntidade.Email))
            {
                throw new Exception("É obrigatório informar o e-mail do usuário.");
            }

            if (string.IsNullOrEmpty(aEntidade.Senha))
            {
                throw new Exception("É obrigatório informar senha do usuário.");
            }

            if (!isEdicao)
            {
                if (!aEntidade.IdUsuarioCadastro.HasValue)
                {
                    throw new Exception("É obrigatório informar o usuário responsável pelo cadastro.");
                }

                VerificarChaves(aEntidade);
            }
        }

        private void VerificarChaves(UsuarioEntidade aEntidade)
        {
            if (new UsuarioDados().BuscarUsuarioPeloEmail(aEntidade.Email) != null)
            {
                throw new Exception("Já existe um usuário cadastrado com esse e-mail.");
            }
        }
    }
}
