using Dados;
using Entidade;
using Enumerador;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var usuario = dados.BuscarUsuario(aEmail);

            switch (usuario.Tipo)
            {
                case TipoUsuarioEnum.Funcionario:
                    return dados.BuscarInfoLoginFuncionario(usuario.Id);
                case TipoUsuarioEnum.Cliente:
                    return dados.BuscarInfoLoginCliente(usuario.Id);
                default:
                    return null;
            }
        }
    }
}
