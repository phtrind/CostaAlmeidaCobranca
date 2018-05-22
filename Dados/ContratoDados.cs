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
    public class ContratoDados : DadosBase<ContratoEntidade>
    {
        public IEnumerable<ContratoEntidade> ListarTodosCompleto()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM CONTRATOS
                                            INNER JOIN EVENTOS
                                            ON CONTRATOS.COD_CODEVENTO = EVENTOS.COD_CODEVENTO
                                            INNER JOIN USUARIOS
                                            ON CONTRATOS.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO ");

            var listaRetorno = DadosParaEntidade(resultado).ToList();

            var dadosCliente = new ClienteDados();
            var dadosParcela = new ParcelaDados();

            listaRetorno.ForEach(x =>
            {
                x.Vendedor = dadosCliente.Listar(x.IdVendedor.Value);
                x.Comprador = dadosCliente.Listar(x.IdComprador.Value);
                x.Parcelas = dadosParcela.ListarTodos().Where(a => a.IdContrato == x.Id).ToList();
            });

            return listaRetorno;
        }

        public ContratoEntidade ListarCompleto(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM CONTRATOS
                                             INNER JOIN EVENTOS
                                             ON CONTRATOS.COD_CODEVENTO = EVENTOS.COD_CODEVENTO
                                             INNER JOIN USUARIOS
                                             ON CONTRATOS.COD_CODUSUARIO = USUARIOS.COD_CODUSUARIO 
                                          WHERE CONTRATOS.COD_CODCONTRATO = {aCodigo} ");

            var retorno = DadosParaEntidade(resultado).FirstOrDefault();

            var dadosCliente = new ClienteDados();
            var dadosParcela = new ParcelaDados();

            retorno.Vendedor = dadosCliente.Listar(retorno.IdVendedor.Value);
            retorno.Comprador = dadosCliente.Listar(retorno.IdComprador.Value);
            retorno.Parcelas = dadosParcela.ListarTodos().Where(a => a.IdContrato == retorno.Id).ToList();

            return retorno;
        }

        private IEnumerable<ContratoEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ContratoEntidade()
            {
                Id = Convert.ToInt64(x.COD_CODCONTRATO),
                Valor = Convert.ToDecimal(x.VAL_VALORCONTRATO),
                TaxaLucro = Convert.ToDecimal(x.VAL_TAXALUCRO),
                Observacao = x.TXT_OBSERVACAO,
                Status = (StatusContratoEnum)Convert.ToInt32(x.IDC_STATUSCONTRATO),
                IdUsuario = Convert.ToInt64(x.COD_CODUSUARIO),
                IdEvento = Convert.ToInt64(x.COD_CODEVENTO),
                IdVendedor = Convert.ToInt64(x.COD_CODVENDEDOR),
                IdComprador = Convert.ToInt64(x.COD_CODCOMPRADOR),
                DataCadastro = x.DTH_CADASTROCONTRATO,
                Usuario = new UsuarioEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODUSUARIO),
                    Email = x.TXT_EMAIL,
                    Senha = x.TXT_SENHA,
                    Tipo = (TipoUsuarioEnum)Convert.ToInt32(x.IDC_TIPOUSUARIO),
                    DataCadastro = x.DTH_CADASTROUSUARIO
                },
                Evento = new EventoEntidade()
                {
                    Id = Convert.ToInt64(x.COD_CODEVENTO),
                    Nome = x.TXT_NOMEEVENTO,
                    Data = x.DTH_DATAEVENTO,
                    IdUsuario = Convert.ToInt64(x.COD_CODUSUARIO),
                    IdEndereco = Convert.ToInt64(x.COD_CODENDERECO),
                    DataCadastro = x.DTH_CADASTROEVENTO
                }
            });
        }
    }
}
