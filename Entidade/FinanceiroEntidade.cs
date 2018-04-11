using Enumerador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class FinanceiroEntidade : EntidadeBase
    {
        public decimal Valor { get; set; }
        public TipoFinanceiroEnum Tipo { get; set; }
        public DateTime Data { get; set; }
        public StatusFinanceiroEnum Status { get; set; }
    }
}
