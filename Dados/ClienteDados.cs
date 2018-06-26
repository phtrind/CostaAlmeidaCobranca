using Dapper;
using Entidade;
using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dados
{
    public class ClienteDados : DadosBase<ClienteEntidade>
    {
        public IEnumerable<ClienteEntidade> ListarTodosCompleto()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM CLI_CLIENTES
                                            INNER JOIN END_ENDERECOS
                                            ON CLI_CLIENTES.END_CODIGO = END_ENDERECOS.END_CODIGO
                                            INNER JOIN USU_USUARIOS
                                            ON CLI_CLIENTES.USU_CODIGO = USU_USUARIOS.USU_CODIGO ");

            return DadosParaEntidade(resultado);
        }

        public ClienteEntidade ListarCompleto(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                        FROM CLI_CLIENTES
                                            INNER JOIN END_ENDERECOS
                                            ON CLI_CLIENTES.END_CODIGO = END_ENDERECOS.END_CODIGO
                                            INNER JOIN USU_USUARIOS
                                            ON CLI_CLIENTES.USU_CODIGO = USU_USUARIOS.USU_CODIGO 
                                         WHERE CLI_CLIENTES.CLI_CODIGO = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<ClienteEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ClienteEntidade()
            {
                Id = Convert.ToInt64(x.CLI_CODIGO),
                Nome = x.CLI_NOME,
                Cpf = x.CLI_CPF,
                Email = x.CLI_EMAIL,
                TelefoneFixo = x.CLI_TELFIXO,
                TelefoneCelular = x.CLI_TELCELULAR,
                DataNascimento = x.CLI_DTHNASCIMENTO,
                IdEndereco = Convert.ToInt64(x.END_CODIGO),
                IdUsuario = Convert.ToInt64(x.USU_CODIGO),
                DataCadastro = Convert.ToDateTime(x.CLI_DTHCADASTRO),
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.USU_CODIGO),
                    Email = x.USU_EMAIL,
                    Senha = x.USU_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.USU_TIPO),
                    DataCadastro = Convert.ToDateTime(x.USU_DTHCADASTRO)
                },
                Endereco = new EnderecoEntidade()
                {
                    Id = Convert.ToInt64(x.END_CODIGO),
                    Logradouro = x.END_LOGRADOURO,
                    Numero = x.END_NUMERO,
                    Complemento = x.END_COMPLEMENTO,
                    Bairro = x.END_BAIRRO,
                    Cep = x.END_CEP,
                    Cidade = x.END_CIDADE,
                    Estado = x.END_ESTADO,
                    DataCadastro = Convert.ToDateTime(x.END_DTHCADASTRO)
                }
            });
        }
    }
}
