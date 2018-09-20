using System;
using System.Collections.Generic;

namespace Projecao
{
    public class BuscarParaEditarParcelaResponse
    {
        public long Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal TaxaLucro { get; set; }
        public int Status { get; set; }
        public decimal? ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }

        public List<ComboProjecao> StatusParcelas { get; set; }
    }
}
