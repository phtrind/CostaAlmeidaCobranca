using Dapper;
using Entidade;
using Enumerador;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace Dados
{
    public class FinanceiroDados : DadosBase<FinanceiroEntidade>
    {
        public override IEnumerable<FinanceiroEntidade> ListarTodos()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM TRA_TRANSACOES
                                            INNER JOIN USU_USUARIOS
                                            ON TRA_TRANSACOES.USU_CODIGO = USU_USUARIOS.USU_CODIGO ");

            return DadosParaEntidade(resultado);
        }

        public override FinanceiroEntidade Listar(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM TRA_TRANSACOES
                                             INNER JOIN USU_USUARIOS
                                             ON TRA_TRANSACOES.USU_CODIGO = USU_USUARIOS.USU_CODIGO
                                         WHERE TRA_CODIGO = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<FinanceiroEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new FinanceiroEntidade()
            {
                Id = Convert.ToInt64(x.TRA_CODIGO),
                Valor = Convert.ToDecimal(x.TRA_VALOR),
                Tipo = (TipoFinanceiroEnum)Convert.ToInt32(x.TRA_TIPO),
                Data = Convert.ToDateTime(x.TRA_DATA),
                Status = (StatusFinanceiroEnum)Convert.ToInt32(x.TRA_STATUS),
                IdUsuario = Convert.ToInt64(x.USU_CODIGO),
                DataCadastro = x.TRA_DTHCADASTRO,
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.USU_CODIGO),
                    Email = x.USU_EMAIL,
                    Senha = x.USU_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.USU_TIPO),
                    DataCadastro = x.USU_DTHCADASTRO
                }
            });
        }
    }
}
