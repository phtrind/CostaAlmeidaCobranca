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
            var resultado = db.Query("SELECT * FROM TRANSACOES INNER JOIN USUARIOS ON TRANSACOES.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO");

            return resultado.Select(financeiro => new FinanceiroEntidade()
            {
                Id = financeiro.COD_CODTRANSACAO,
                Valor = Convert.ToDecimal(financeiro.VAL_VALOR),
                Tipo = (TipoFinanceiroEnum)Convert.ToInt32(financeiro.IDC_TIPO),
                Data = Convert.ToDateTime(financeiro.DTH_DATA),
                Status = (StatusFinanceiroEnum)Convert.ToInt32(financeiro.IDC_STATUS),
                IdUsuario = financeiro.COD_CODUSUARIO,
                DataCadastro = financeiro.DTH_DATACADASTRO,
                Usuario = new UsuarioEntidade()
                {
                    Id = financeiro.COD_CODUSUARIO,
                    Email = financeiro.TXT_EMAIL,
                    Senha = financeiro.TXT_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(financeiro.IDC_TIPOUSUARIO),
                    DataCadastro = financeiro.DTH_DATACADASTRO
                }
            });
        }
    }
}
