using Enumerador;
using System;

namespace Entidade
{
    public class ParcelasEntidade : EntidadeBase
    {
        public long? Id { get; set; }

        public decimal Valor { get; set; }
        public decimal TaxaLucro { get; set; }
        public DateTime Vencimento { get; set; }
        public StatusParcelaEnum Status { get; set; }
        public decimal? ValorPago { get; set; }
        public DateTime? DataPagamento { get; set; }

        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

        public long? IdContrato { get; set; }
        public long? IdUsuarioCadastro { get; set; }
        public long? IdUsuarioAlteracao { get; set; }

        public ContratoEntidade Contrato { get; set; }
        public UsuarioEntidade UsuarioCadastro { get; set; }
        public UsuarioEntidade UsuarioAlteracao { get; set; }
    }
}
