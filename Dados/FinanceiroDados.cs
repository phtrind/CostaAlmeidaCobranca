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
                                        FROM TRANSACOES
                                            INNER JOIN USUARIOS
                                            ON TRANSACOES.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO ");

            return DadosParaEntidade(resultado);
        }

        public override FinanceiroEntidade Listar(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM TRANSACOES
                                             INNER JOIN USUARIOS
                                             ON TRANSACOES.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO
                                         WHERE COD_CODTRANSACAO = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<FinanceiroEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new FinanceiroEntidade()
            {
                Id = Convert.ToInt64(x.COD_CODTRANSACAO),
                Valor = Convert.ToDecimal(x.VAL_VALOR),
                Tipo = (TipoFinanceiroEnum)Convert.ToInt32(x.IDC_TIPO),
                Data = Convert.ToDateTime(x.DTH_DATA),
                Status = (StatusFinanceiroEnum)Convert.ToInt32(x.IDC_STATUS),
                IdUsuario = Convert.ToInt64(x.COD_CODUSUARIO),
                DataCadastro = x.DTH_CADASTROTRANSACAO,
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODUSUARIO),
                    Email = x.TXT_EMAIL,
                    Senha = x.TXT_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.IDC_TIPOUSUARIO),
                    DataCadastro = x.DTH_CADASTROUSUARIO
                }
            });
        }
    }
}
