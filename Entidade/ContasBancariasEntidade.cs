using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class ContasBancariasEntidade : EntidadeBase
    {
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }
        public ClienteEntidade Cliente { get; set; }
    }
}
