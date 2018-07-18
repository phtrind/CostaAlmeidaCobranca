using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projecao
{
    public class RelatorioContratoResponse
    {
        public string Id { get; set; }
        public string Vendedor { get; set; }
        public string Comprador { get; set; }
        public string Evento { get; set; }
        public string Valor { get; set; }
        public string Status { get; set; }
        public string Parcelas { get; set; }
    }
}
