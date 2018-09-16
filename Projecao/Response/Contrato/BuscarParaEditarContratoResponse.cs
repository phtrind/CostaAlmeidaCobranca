using System.Collections.Generic;

namespace Projecao
{
    public class BuscarParaEditarContratoResponse
    {
        public long Id { get; set; }
        public long IdVendedor { get; set; }
        public long IdComprador { get; set; }
        public string Animal { get; set; }
        public string Observação { get; set; }
        public string IdEvento { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeParcelas { get; set; }
        public List<BuscarParaEditarParcelaResponse> Parcelas { get; set; }
    }
}
