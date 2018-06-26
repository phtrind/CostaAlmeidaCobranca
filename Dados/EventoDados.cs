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
    public class EventoDados : DadosBase<EventoEntidade>
    {
        public override IEnumerable<EventoEntidade> ListarTodos()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM EVE_EVENTOS
                                            INNER JOIN USU_USUARIOS
                                            ON EVE_EVENTOS.USU_CODIGO = USU_USUARIOS.USU_CODIGO
                                            INNER JOIN END_ENDERECOS
                                            ON EVE_EVENTOS.END_CODIGO = END_ENDERECOS.END_CODIGO ");

            return DadosParaEntidade(resultado);
        }

        public override EventoEntidade Listar(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                        FROM EVE_EVENTOS
                                            INNER JOIN USU_USUARIOS
                                            ON EVE_EVENTOS.USU_CODIGO = USU_USUARIOS.USU_CODIGO
                                            INNER JOIN END_ENDERECOS
                                            ON EVE_EVENTOS.END_CODIGO = END_ENDERECOS.END_CODIGO 
                                         WHERE EVE_EVENTOS.EVE_CODIGO = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<EventoEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new EventoEntidade()
            {
                Id = Convert.ToInt64(x.EVE_CODIGO),
                Nome = x.EVE_NOME,
                Data = x.EVE_DATA,
                IdUsuario = Convert.ToInt64(x.USU_CODIGO),
                IdEndereco = Convert.ToInt64(x.END_CODIGO),
                DataCadastro = x.EVE_DTHCADASTRO,
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.USU_CODIGO),
                    Email = x.USU_EMAIL,
                    Senha = x.USU_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.USU_TIPO),
                    DataCadastro = x.USU_DTHCADASTRO
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
