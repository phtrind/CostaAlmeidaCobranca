using Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dommel;
using Projecao;
using Enumerador;

namespace Dados
{
    public class UsuarioDados : DadosBase<UsuarioEntidade>
    {
        public IEnumerable<UsuarioEntidade> BuscarLogin(string aEmail, string aSenha)
        {
            return db.Select<UsuarioEntidade>(x => x.Email == aEmail && x.Senha == aSenha);
        }

        public UsuarioEntidade BuscarUsuarioPeloEmail(string aEmail)
        {
            return db.Select<UsuarioEntidade>(x => x.Email == aEmail).FirstOrDefault();
        }

        public InformacoesUsuarioResponse BuscarInfoLoginFuncionario(long? id)
        {
            var resultado = db.QueryFirstOrDefault($@"SELECT F.FUN_CODIGO, F.FUN_NOME, FUN_PERMISSAO
                                                      FROM FUN_FUNCIONARIOS F
                                                          INNER JOIN USU_USUARIOS U
                                                          ON F.USU_CODIGO = U.USU_CODIGO
                                                      WHERE F.USU_CODIGO = {id.Value}");

            if (resultado != null)
            {
                return new InformacoesUsuarioResponse()
                {
                    IdInterno = Convert.ToInt64(resultado.FUN_CODIGO),
                    Nome = resultado.FUN_NOME,
                    Permissao = Convert.ToInt32(resultado.FUN_PERMISSAO),
                    TipoUsuario = TipoUsuarioEnum.Funcionario.GetHashCode(),
                    IdUsuario = id.Value
                };
            }

            return null;
        }

        public InformacoesUsuarioResponse BuscarInfoLoginCliente(long? id)
        {
            var resultado = db.QueryFirstOrDefault($@"SELECT C.CLI_CODIGO, C.CLI_NOME
                                                      FROM CLI_CLIENTES C
                                                          INNER JOIN USU_USUARIOS U
                                                          ON C.USU_CODIGO = U.USU_CODIGO
                                                      WHERE C.USU_CODIGO = {id.Value}");

            if (resultado != null)
            {
                return new InformacoesUsuarioResponse()
                {
                    IdInterno = Convert.ToInt64(resultado.CLI_CODIGO),
                    Nome = resultado.CLI_NOME,
                    TipoUsuario = TipoUsuarioEnum.Cliente.GetHashCode(),
                    IdUsuario = id.Value
                };
            }

            return null;
        }
    }
}
