using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class EventoEntidade : EntidadeBase
    {
        public string Nome { get; set; }
        public DateTime Data { get; set; }
        public EnderecoEntidade Endereco { get; set; }
        public List<ContratoEntidade> Contratos { get; set; }
    }
}
