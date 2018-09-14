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
                                        FROM CON_CONTRATOS
                                            LEFT JOIN EVE_EVENTOS
                                            ON CON_CONTRATOS.EVE_CODIGO = EVE_EVENTOS.EVE_CODIGO
                                            INNER JOIN USU_USUARIOS
                                            ON CON_CONTRATOS.USU_CODIGO = USU_USUARIOS.USU_CODIGO ");

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
                                         FROM CON_CONTRATOS
                                             LEFT JOIN EVE_EVENTOS
                                             ON CON_CONTRATOS.EVE_CODIGO = EVE_EVENTOS.EVE_CODIGO
                                             INNER JOIN USU_USUARIOS
                                             ON CON_CONTRATOS.USU_CODIGO = USU_USUARIOS.USU_CODIGO 
                                          WHERE CON_CONTRATOS.CON_CODIGO = {aCodigo} ");

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
            var retorno = new List<ContratoEntidade>();

            foreach (var x in aResultado)
            {
                var contrato = new ContratoEntidade()
                {
                    Id = Convert.ToInt64(x.CON_CODIGO),
                    Valor = Convert.ToDecimal(x.CON_VALOR),
                    Animal = x.CON_ANIMAL,
                    Observacao = x.CON_OBSERVACAO,
                    Status = (StatusContratoEnum)Convert.ToInt32(x.CON_STATUS),
                    IdUsuarioCadastro = Convert.ToInt64(x.USU_CADASTRO),
                    IdEvento = Convert.ToInt64(x.EVE_CODIGO),
                    IdVendedor = Convert.ToInt64(x.CON_VENDEDOR),
                    IdComprador = Convert.ToInt64(x.CON_COMPRADOR),
                    DataCadastro = Convert.ToDateTime(x.CON_DTHCADASTRO),
                    DataAlteracao = Convert.ToDateTime(x.CON_DTHALTERACAO)
                };

                if (x.EVE_CODIGO != null)
                {
                    contrato.Evento = new EventoEntidade()
                    {
                        Id = Convert.ToInt64(x.EVE_CODIGO),
                        Nome = x.EVE_NOME,
                        Data = x.EVE_DATA,
                        IdEndereco = Convert.ToInt64(x.END_CODIGO),
                        DataCadastro = x.EVE_DTHCADASTRO
                    };
                }

                retorno.Add(contrato);
            }

            return retorno;
        }

        public IEnumerable<dynamic> Relatorio()
        {
            return db.Query($@" SELECT *
                                FROM CON_CONTRATOS
                                    LEFT JOIN EVE_EVENTOS
                                    ON CON_CONTRATOS.EVE_CODIGO = EVE_EVENTOS.EVE_CODIGO
                                    INNER JOIN USU_USUARIOS
                                    ON CON_CONTRATOS.USU_CODIGO = USU_USUARIOS.USU_CODIGO");
        }
    }
}
