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
                                        FROM EVENTOS
                                            INNER JOIN USUARIOS
                                            ON EVENTOS.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO
                                            INNER JOIN ENDERECOS
                                            ON EVENTOS.COD_CODENDERECO = ENDERECOS.COD_CODENDERECO ");

            return DadosParaEntidade(resultado);
        }

        public override EventoEntidade Listar(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                        FROM EVENTOS
                                            INNER JOIN USUARIOS
                                            ON EVENTOS.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO
                                            INNER JOIN ENDERECOS
                                            ON EVENTOS.COD_CODENDERECO = ENDERECOS.COD_CODENDERECO 
                                         WHERE EVENTOS.COD_CODEVENTO = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<EventoEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new EventoEntidade()
            {
                Id = Convert.ToInt64(x.COD_CODEVENTO),
                Nome = x.TXT_NOMEEVENTO,
                Data = x.DTH_DATAEVENTO,
                IdUsuario = Convert.ToInt64(x.COD_CODUSUARIO),
                IdEndereco = Convert.ToInt64(x.COD_CODENDERECO),
                DataCadastro = x.DTH_CADASTROEVENTO,
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODUSUARIO),
                    Email = x.TXT_EMAIL,
                    Senha = x.TXT_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.IDC_TIPOUSUARIO),
                    DataCadastro = x.DTH_CADASTROUSUARIO
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
