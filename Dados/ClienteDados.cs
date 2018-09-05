using Dapper;
using Entidade;
using Projecao;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dados
{
    public class ClienteDados : DadosBase<ClienteEntidade>
    {
        public IEnumerable<ComboProjecao> getComboClientes()
        {
            var resultado = db.Query($@"SELECT CLI_CODIGO, CLI_NOME
                                        FROM CLI_CLIENTES
                                        ORDER BY CLI_NOME");

            return resultado.Select(x => new ComboProjecao()
            {
                Codigo = Convert.ToInt64(x.CLI_CODIGO),
                Descricao = x.CLI_NOME
            });
        }
    }
}
