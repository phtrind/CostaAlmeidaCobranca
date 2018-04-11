using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class ParcelasEntidade : EntidadeBase
    {
        public decimal Valor { get; set; }
        public DateTime Vencimento { get; set; }
        public StatusParcelaEnum Status { get; set; }
        public decimal Juros { get; set; }
        public ContratoEntidade Contrato { get; set; }
    }
}
