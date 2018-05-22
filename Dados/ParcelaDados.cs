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
    public class ParcelaDados : DadosBase<ParcelasEntidade>
    {
        public IEnumerable<ParcelasEntidade> ListarTodosCompleto()
        {
            var resultado = db.Query(@" SELECT *
                                        FROM PARCELAS
                                            INNER JOIN CONTRATOS
                                            ON PARCELAS.COD_CODCONTRATO = CONTRATOS.COD_CODCONTRATO ");

            return DadosParaEntidade(resultado);
        }

        public ParcelasEntidade ListarCompleto(long aCodigo)
        {
            var resultado = db.Query($@" SELECT *
                                         FROM PARCELAS
                                             INNER JOIN CONTRATOS
                                             ON PARCELAS.COD_CODCONTRATO = CONTRATOS.COD_CODCONTRATO 
                                         WHERE PARCELAS.COD_CODPARCELA = {aCodigo} ");

            return DadosParaEntidade(resultado).FirstOrDefault();
        }

        private IEnumerable<ParcelasEntidade> DadosParaEntidade(IEnumerable<dynamic> aResultado)
        {
            return aResultado.Select(x => new ParcelasEntidade()
            {
                Id = Convert.ToInt64(x.COD_CODPARCELA),
                Valor = Convert.ToDecimal(x.VAL_VALORPARCELA),
                Vencimento = x.DTH_VENCIMENTO,
                Status = (StatusParcelaEnum)Convert.ToInt32(x.IDC_STATUSPARCELA),
                Juros = Convert.ToDecimal(x.VAL_JUROSPARCELA),
                IdContrato = Convert.ToInt64(x.COD_CODCONTRATO),
                DataCadastro = Convert.ToDateTime(x.DTH_CADASTROPARCELA),
                Contrato = new ContratoEntidade()
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
                    DataCadastro = x.DTH_CADASTROCONTRATO
                }
            });
        }
    }
}
