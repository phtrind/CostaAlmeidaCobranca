using Dapper;
using Dommel;
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

        public ClienteEntidade BuscarClientePeloEmail(string aEmail)
        {
            return db.Select<ClienteEntidade>(x => x.Email == aEmail).FirstOrDefault();
        }

        public ClienteEntidade BuscarClientePeloCpfCnpj(string aCpf)
        {
            return db.Select<ClienteEntidade>(x => x.Cpf == aCpf).FirstOrDefault();
        }

        public dynamic RelatorioDetalhado(long idCliente)
        {
            return db.Query($@"SELECT C.CLI_CODIGO, 
                                      C.CLI_NOME, 
                                      C.CLI_EMAIL, 
                                      C.CLI_FAZENDA, 
                                      C.CLI_CPF, 
                                      C.CLI_TELFIXO, 
                                      C.CLI_TELCELULAR, 
                                      E.END_CODIGO, 
                                      E.END_CEP, 
                                      E.END_LOGRADOURO, 
                                      E.END_NUMERO, 
                                      E.END_COMPLEMENTO, 
                                      E.END_BAIRRO, 
                                      E.END_ESTADO, 
                                      E.END_CIDADE
                               FROM CLI_CLIENTES C
                                    INNER JOIN END_ENDERECOS E ON C.END_CODIGO = E.END_CODIGO
                               WHERE C.CLI_CODIGO = {idCliente};").FirstOrDefault();
        }
    }
}
