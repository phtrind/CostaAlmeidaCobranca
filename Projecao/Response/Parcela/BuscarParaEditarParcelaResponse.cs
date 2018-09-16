using System;

namespace Projecao
{
    public class BuscarParaEditarParcelaResponse
    {
        public long Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal TaxaLucro { get; set; }
    }
}
