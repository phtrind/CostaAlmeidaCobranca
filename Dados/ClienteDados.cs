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
                                        FROM CLIENTES
                                            INNER JOIN ENDERECOS
                                            ON CLIENTES.COD_CODENDERECO = ENDERECOS.COD_CODENDERECO
                                            INNER JOIN USUARIOS
                                            ON CLIENTES.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO ");

            return DadosParaEntidade(resultado);
        }

        public ClienteEntidade ListarCompleto(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM CLIENTES
                                             INNER JOIN ENDERECOS
                                             ON CLIENTES.COD_CODENDERECO = ENDERECOS.COD_CODENDERECO
                                             INNER JOIN USUARIOS
                                             ON CLIENTES.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO 
                                         WHERE CLIENTES.COD_CODCLIENTE = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<ClienteEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ClienteEntidade()
            {
                Id = Convert.ToInt64(x.COD_CODCLIENTE),
                Nome = x.TXT_NOME,
                Cpf = x.TXT_CPF,
                Email = x.TXT_EMAIL,
                TelefoneFixo = x.TEL_TELEFONEFIXO,
                TelefoneCelular = x.TEL_TELEFONECELULAR,
                DataNascimento = x.DTH_DATANASCIMENTO,
                IdEndereco = Convert.ToInt64(x.COD_CODENDERECO),
                IdUsuario = Convert.ToInt64(x.COD_CODUSUARIO),
                DataCadastro = Convert.ToDateTime(x.DTH_CADASTROCLIENTE),
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODUSUARIO),
                    Email = x.TXT_EMAIL,
                    Senha = x.TXT_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.IDC_TIPOUSUARIO),
                    DataCadastro = Convert.ToDateTime(x.DTH_CADASTROUSUARIO)
                },
                Endereco = new EnderecoEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODENDERECO),
                    Logradouro = x.TXT_LOGRADOURO,
                    Numero = x.TXT_NUMERO,
                    Complemento = x.TXT_COMPLEMENTO,
                    Cep = x.NUM_CEP,
                    Cidade = x.TXT_CIDADE,
                    Estado = x.TXT_ESTADO,
                    DataCadastro = Convert.ToDateTime(x.DTH_CADASTROENDERECO)
                }
            });
        }
    }
}
